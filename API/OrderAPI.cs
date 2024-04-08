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

            app.MapGet("/orderItems", (HHPWBEDbContext db) => //get all orders with items (not necessary/ only for testing purposes)
            {
                return db.Orders.Include(i => i.Items).ThenInclude(i => i.Item).ToList();
            });

            app.MapGet("/orders/{orderId}", (HHPWBEDbContext db, int orderId) => //get single order with items by orderId
            {
                return db.Orders.Include(i => i.Items).ThenInclude(z => z.Item).FirstOrDefault(o => o.Id == orderId);
            });

            app.MapPost("/orders", (HHPWBEDbContext db, CreateOrderDTO newOrder) => // create new order
            {
                Order orderToCreate = new()
                {
                    CustomerName = newOrder.CustomerName,
                    CustomerEmail = newOrder.CustomerEmail,
                    CustomerPhone = newOrder.CustomerPhone,
                    OrderType = newOrder.OrderType,
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
                 
                    orderToUpdate.OrderType = updatedOrder.OrderType;
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