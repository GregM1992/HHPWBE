using HHPWBE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HHPWBE.API
{
    public static class OrderTypeAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/orderType", (HHPWBEDbContext db) =>
            {
                return db.OrderTypes.ToList();
            });
        }
    }
}
