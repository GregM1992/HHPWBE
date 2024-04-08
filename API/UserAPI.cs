using HHPWBE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace HHPWBE.API
{
    public static class UserAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/checkuser/{Uid}", (HHPWBEDbContext db, string Uid) =>
            {
                var user = db.Users.Where(user => user.Uid == Uid).ToList();

                if (Uid == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(user);
                }
            });

            app.MapPost("/register", (HHPWBEDbContext db, User newUser) => // creates user entity
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Results.Created($"/user/{newUser.Id}", newUser);
            });

        }
    }
}