﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Test.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241111093256_CategoryResponseModel")]
    partial class CategoryResponseModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Electronics"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Books"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Clothing"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Furniture"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryName = "Sports & Outdoors"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryName = "Toys"
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryName = "Beauty & Health"
                        },
                        new
                        {
                            CategoryId = 8,
                            CategoryName = "Home Appliances"
                        },
                        new
                        {
                            CategoryId = 9,
                            CategoryName = "Automotive"
                        },
                        new
                        {
                            CategoryId = 10,
                            CategoryName = "Grocery"
                        },
                        new
                        {
                            CategoryId = 11,
                            CategoryName = "Music"
                        });
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            ProductName = "Laptop"
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            ProductName = "Smartphone"
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 1,
                            ProductName = "Earpod"
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 1,
                            ProductName = "Speaker"
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 1,
                            ProductName = "Headphones"
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 2,
                            ProductName = "Novel"
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 2,
                            ProductName = "Science Fiction Book"
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 2,
                            ProductName = "Literature"
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 2,
                            ProductName = "Textbook"
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 3,
                            ProductName = "T-Shirt"
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 3,
                            ProductName = "Jeans"
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 3,
                            ProductName = "Jacket"
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 3,
                            ProductName = "Sweater"
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 4,
                            ProductName = "Dining Table"
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 4,
                            ProductName = "Chair"
                        });
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.HasOne("Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
