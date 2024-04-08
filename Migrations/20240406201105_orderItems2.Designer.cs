﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HHPWBE.Migrations
{
    [DbContext(typeof(HHPWBEDbContext))]
    [Migration("20240406201105_orderItems2")]
    partial class orderItems2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HHPWBE.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "6pc Wings",
                            Price = 6.00m
                        },
                        new
                        {
                            Id = 2,
                            Name = "12pc Wings",
                            Price = 10.00m
                        },
                        new
                        {
                            Id = 3,
                            Name = "14in Cheese",
                            Price = 13.00m
                        },
                        new
                        {
                            Id = 4,
                            Name = "18in Cheese",
                            Price = 15.00m
                        },
                        new
                        {
                            Id = 5,
                            Name = "14in Pepperoni",
                            Price = 15.00m
                        },
                        new
                        {
                            Id = 6,
                            Name = "18in Pepperoni",
                            Price = 20.00m
                        });
                });

            modelBuilder.Entity("HHPWBE.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateClosed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<int>("OrderType")
                        .HasColumnType("integer");

                    b.Property<decimal?>("SubTotal")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Tip")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerEmail = "verygood@eng.com",
                            CustomerName = "John Man",
                            CustomerPhone = "612-343-2345",
                            DateCreated = new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsClosed = false,
                            OrderType = 1
                        },
                        new
                        {
                            Id = 2,
                            CustomerEmail = "soCool@32.com",
                            CustomerName = "Jeff Dude",
                            CustomerPhone = "762-232-3234",
                            DateClosed = new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateCreated = new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsClosed = true,
                            OrderType = 2,
                            SubTotal = 20.00m,
                            Tip = 5.00m
                        });
                });

            modelBuilder.Entity("HHPWBE.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ItemInOrderId")
                        .HasColumnType("integer");

                    b.Property<int?>("OrderContainingItemId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemInOrderId");

                    b.HasIndex("OrderContainingItemId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("HHPWBE.Models.OrderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Walk-In"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Call-In"
                        });
                });

            modelBuilder.Entity("HHPWBE.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Uid = "fgjfqhqAvZNUIbI0Z3v9McdQxNy1"
                        });
                });

            modelBuilder.Entity("HHPWBE.Models.OrderItem", b =>
                {
                    b.HasOne("HHPWBE.Models.Item", "ItemInOrder")
                        .WithMany("Orders")
                        .HasForeignKey("ItemInOrderId");

                    b.HasOne("HHPWBE.Models.Order", "OrderContainingItem")
                        .WithMany("Items")
                        .HasForeignKey("OrderContainingItemId");

                    b.Navigation("ItemInOrder");

                    b.Navigation("OrderContainingItem");
                });

            modelBuilder.Entity("HHPWBE.Models.Item", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HHPWBE.Models.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
