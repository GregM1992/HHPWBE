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

            app.MapGet("/orderSubTotal/{id}", (HHPWBEDbContext db, int id) => //gets order subtotal
            {
                var order = db.Orders
                         .Include(order => order.Items)
                         .ThenInclude(orderItem => orderItem.Item)
                         .SingleOrDefault(order => order.Id == id);

                return order.SubTotal;
            });

            app.MapGet("/orderTotal/{id}", (HHPWBEDbContext db, int id) => //gets order total
            {
                var order = db.Orders
                         .Include(order => order.Items)
                         .ThenInclude(orderItem => orderItem.Item)
                         .SingleOrDefault(order => order.Id == id);

                return order.Total;
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
                Order orderToCreate = new()
                {
                    CustomerName = newOrder.CustomerName,
                    CustomerEmail = newOrder.CustomerEmail,
                    CustomerPhone = newOrder.CustomerPhone,
                    OrderTypeId = newOrder.OrderTypeId,
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
                    return Results.Created();
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapDelete("/orders/{orderId}", (HHPWBEDbContext db, int orderId) => // delete order
            {
                var orderToDelete = db.Orders.FirstOrDefault(i => i.Id == orderId);
                if (orderToDelete != null)
                {
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
                var orderToUpdate = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (orderToUpdate != null)
                {
                    orderToUpdate.CustomerName = updatedOrder.CustomerName;
                    orderToUpdate.CustomerEmail = updatedOrder.CustomerEmail;
                    orderToUpdate.CustomerPhone = updatedOrder.CustomerPhone;

                    orderToUpdate.OrderTypeId = updatedOrder.OrderType;
                    db.SaveChanges();
                    return Results.Ok();
                }
                else
                {
                    return Results.NotFound();
                }
            });
        }
    }
}