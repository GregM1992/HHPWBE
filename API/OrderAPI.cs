using HHPWBE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HHPWBE.API
{
    public static class OrderAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/orders", (HHPWBEDbContext db) => //get all orders without items
            {
                return db.Orders.ToList();
            });

            app.MapGet("/orderWithType", (HHPWBEDbContext db) => //get all orders with orderType
            {
                return db.Orders.Include(i => i.OrderType).ToList();
            });

            app.MapGet("/orderEditDetails/{id}", (HHPWBEDbContext db, int id) =>
            {
                var orderToUpdate = db.Orders.Include(o => o.OrderType).FirstOrDefault(o => o.Id == id);
                return new OrderDTO
                {
                    id = id,
                    customerName = orderToUpdate.CustomerName,
                    customerEmail = orderToUpdate.CustomerEmail,
                    customerPhone = orderToUpdate.CustomerPhone,
                    orderTypeId = orderToUpdate.OrderTypeId,
                    dateCreated = orderToUpdate.DateCreated,
                };
            });


            app.MapGet("/orderTotal/{id}", (HHPWBEDbContext db, int id) => //gets order total
            {
                var order = db.Orders
                         .Include(order => order.Items)
                         .ThenInclude(orderItem => orderItem.Item)
                         .SingleOrDefault(order => order.Id == id);


              return new OrderTotalDTO 
                {
                    Total = order.Total,
                    SubTotal = order.SubTotal,
                    Tip = order.Tip,
                };
            });

            app.MapGet("/orderStatus/{id}", (HHPWBEDbContext db, int id) =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == id);

        
                    return new OrderStatusDTO
                    {
                        OrderStatus = order.IsClosed
                    };
                

            });




           app.MapGet("/orders/{orderId}", (HHPWBEDbContext db, int orderId) => //get single order with specific properties
            {
                return db.Orders.Where(order => order.Id == orderId).Select(order => new {
                                                                                          OrderId = order.Id,
                                                                                          CustomerName = order.CustomerName,
                                                                                          CustomerPhone = order.CustomerPhone,

                                                                                          Items = order.Items.Select(oi => new ItemDTO
                                                                                         {
                                                                                          Id = oi.Item.Id,
                                                                                          OrderItemId = oi.Id,
                                                                                          Name = oi.Item.Name,
                                                                                          Price = oi.Item.Price,
                                                                                         }).ToList()
                                                                                         }).FirstOrDefault();
                                                                                         });

            app.MapGet("/orders/items/{orderId}", (HHPWBEDbContext db, int orderId) => // gets items in order without order info
            {
                var orderItems = db.OrderItem
                    .Where(oi => oi.Order.Id == orderId)
                    .Select(oi => new
                    {
                        OrderId = oi.Order.Id,
                        Name = oi.Item.Name,
                        OrderItemId = oi.Id,
                        Price = oi.Item.Price,
                        IsClosed = oi.Order.IsClosed
                    })
                    .ToList();

                return orderItems;
            });

            app.MapPost("/orders", (HHPWBEDbContext db, CreateOrderDTO newOrder) => // create new order
            {
                var orderTypeToApply = db.OrderTypes.FirstOrDefault(o => o.Id == newOrder.OrderTypeId);

                Order orderToCreate = new()
                {
                    CustomerName = newOrder.CustomerName,
                    CustomerEmail = newOrder.CustomerEmail,
                    CustomerPhone = newOrder.CustomerPhone,
                    OrderType = orderTypeToApply,
                    DateCreated = DateTime.Now,
                    IsClosed = false,
                };
                db.Orders.Add(orderToCreate);
                db.SaveChanges();
                return Results.Created($"/orders/{orderToCreate.Id}", newOrder);
            });

            app.MapPut("/orders/{orderId}", (HHPWBEDbContext db, int orderId, CloseOrderDTO closedOrder) => //close order 
            {
                var orderToClose = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (orderToClose != null)
                {
                    orderToClose.DateClosed = closedOrder.DateClosed;
                    orderToClose.IsClosed = true;
                    orderToClose.Tip = closedOrder.Tip;
                    db.SaveChanges();
                    return Results.Created($"/orders/{orderToClose.Id}", closedOrder);
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapDelete("/orders/{orderId}", (HHPWBEDbContext db, int orderId) =>
            {
                var orderToDelete = db.Orders.Include(o => o.Items).FirstOrDefault(i => i.Id == orderId);

                if (orderToDelete != null)
                {
                    // Remove order items
                    foreach (var item in orderToDelete.Items)
                    {
                        db.OrderItem.Remove(item);
                    }

                    // Remove order
                    db.Orders.Remove(orderToDelete);

                    db.SaveChanges();
                    return Results.Ok();
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapPut("/orders/{orderId}/update", (HHPWBEDbContext db, int orderId, UpdateOrderDTO updatedOrder) =>
            {
                var orderToUpdate = db.Orders.Include(o => o.OrderType).FirstOrDefault(o => o.Id == orderId);
                var orderTypeToApply = db.OrderTypes.FirstOrDefault(o => o.Id == updatedOrder.OrderTypeId);
                if (orderToUpdate != null)
                {
                    orderToUpdate.Id = orderId;
                    orderToUpdate.CustomerName = updatedOrder.CustomerName;
                    orderToUpdate.CustomerEmail = updatedOrder.CustomerEmail;
                    orderToUpdate.CustomerPhone = updatedOrder.CustomerPhone;
                    orderToUpdate.DateCreated = updatedOrder.Created;
                    orderToUpdate.OrderType = orderTypeToApply;
                    db.SaveChanges();
                    return Results.Created($"/orders/{orderToUpdate.Id}", updatedOrder);
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapGet("/revenue", (HHPWBEDbContext db) =>

            {
                var listOfTotals = db.Orders.Where(o => o.IsClosed == true)
                         .Include(order => order.Items)
                         .ThenInclude(orderItem => orderItem.Item).Select(i => i.Total);
                var listOfTips = db.Orders.Where(o => o.IsClosed == true)
                         .Include(order => order.Items)
                         .ThenInclude(orderItem => orderItem.Item).Select(i => i.Tip);
                var listOfTotalsFromItems = db.Orders.Where(o => o.IsClosed == true)
                         .Include(order => order.Items)
                         .ThenInclude(orderItem => orderItem.Item).Select(i => i.SubTotal);
                var listOfWalkins = db.Orders.Where(o => o.IsClosed == true).Where(o => o.OrderTypeId == 1).Select(o => o.OrderTypeId).ToList();
                var listOfCallins = db.Orders.Where(o => o.IsClosed == true).Where(o => o.OrderTypeId == 2).Select(o => o.OrderTypeId).ToList();


                decimal? total = 0;
                decimal? totalTips = 0;
                decimal? totalFromItems = 0;
                int? totalCallins = listOfCallins.Count();
                int? totalWalkins = listOfWalkins.Count();

                foreach (decimal? item in listOfTotals)
                {
                    total += item;
                }
                foreach (decimal? item in listOfTips)
                {
                    totalTips += item;
                }
                foreach (decimal? item in listOfTotalsFromItems)
                {
                    totalFromItems += item;
                }


                RevenueDTO revenueDTO = new RevenueDTO();
                {
                    revenueDTO.TotalFromItems = totalFromItems;
                    revenueDTO.Tips = totalTips;
                    revenueDTO.Total = total;
                    revenueDTO.WalkIns = totalWalkins;
                    revenueDTO.CallIns = totalCallins;
                }
                return revenueDTO;


            });

        }
    }
}