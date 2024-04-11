using HHPWBE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HHPWBE.API
{
    public static class OrderItemAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/order/addItem", (HHPWBEDbContext db, AddItemDTO orderItem) =>
            {
                Order orderToAddTo = db.Orders.FirstOrDefault(o => o.Id == orderItem.OrderId);

                Item itemToAdd = db.Items.FirstOrDefault(i => i.Id == orderItem.ItemId);

                OrderItem newOrderItem = new();

                newOrderItem.Order = orderToAddTo;
                newOrderItem.Item = itemToAdd;

                orderToAddTo.Items.Add(newOrderItem);

                db.SaveChanges();

                return Results.Created($"/order/{orderItem.ItemId}",orderItem);

            });

            app.MapPost("/order/removeItem", (HHPWBEDbContext db, AddItemDTO orderItemToRemove ) =>
            {
                Order orderToDeleteItemFrom = db.Orders.FirstOrDefault(o => o.Id == orderItemToRemove.OrderId);

                Item itemToRemoveFromOrder = db.Items.FirstOrDefault(i => i.Id == orderItemToRemove.ItemId);

                OrderItem orderItem = db.OrderItem.FirstOrDefault(i => i.Item.Id == itemToRemoveFromOrder.Id && i.Order.Id == orderToDeleteItemFrom.Id);

                orderToDeleteItemFrom.Items.Remove(orderItem);
                
                db.SaveChanges();
                
                return Results.Ok();

            });

            app.MapDelete("/order/removeById/{orderItemId}", (HHPWBEDbContext db, int orderItemId) =>
            {
                OrderItem orderItem = db.OrderItem.FirstOrDefault(i => i.Id == orderItemId);

                db.OrderItem.Remove(orderItem);
                db.SaveChanges();
                return Results.Ok();

            });
        }
    }
}


