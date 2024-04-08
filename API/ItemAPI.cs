using HHPWBE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HHPWBE.API
{
    public static class ItemAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/items", (HHPWBEDbContext db) =>
            {
                return db.Items.ToList();
            });
        }
    }
}