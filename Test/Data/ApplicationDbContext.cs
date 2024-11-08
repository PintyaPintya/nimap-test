using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Electronics" },
            new Category { CategoryId = 2, CategoryName = "Books" },
            new Category { CategoryId = 3, CategoryName = "Clothing" },
            new Category { CategoryId = 4, CategoryName = "Furniture" },
            new Category { CategoryId = 5, CategoryName = "Sports & Outdoors" },
            new Category { CategoryId = 6, CategoryName = "Toys" },
            new Category { CategoryId = 7, CategoryName = "Beauty & Health" },
            new Category { CategoryId = 8, CategoryName = "Home Appliances" },
            new Category { CategoryId = 9, CategoryName = "Automotive" },
            new Category { CategoryId = 10, CategoryName = "Grocery" },
            new Category { CategoryId = 11, CategoryName = "Music" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, ProductName = "Laptop", CategoryId = 1 },
            new Product { ProductId = 2, ProductName = "Smartphone", CategoryId = 1 },
            new Product { ProductId = 3, ProductName = "Earpod", CategoryId = 1 },
            new Product { ProductId = 4, ProductName = "Speaker", CategoryId = 1 },
            new Product { ProductId = 5, ProductName = "Headphones", CategoryId = 1 },
            new Product { ProductId = 6, ProductName = "Novel", CategoryId = 2 },
            new Product { ProductId = 7, ProductName = "Science Fiction Book", CategoryId = 2 },
            new Product { ProductId = 8, ProductName = "Literature", CategoryId = 2 },
            new Product { ProductId = 9, ProductName = "Textbook", CategoryId = 2 },
            new Product { ProductId = 10, ProductName = "T-Shirt", CategoryId = 3 },
            new Product { ProductId = 11, ProductName = "Jeans", CategoryId = 3 },
            new Product { ProductId = 12, ProductName = "Jacket", CategoryId = 3 },
            new Product { ProductId = 13, ProductName = "Sweater", CategoryId = 3 },
            new Product { ProductId = 14, ProductName = "Dining Table", CategoryId = 4 },
            new Product { ProductId = 15, ProductName = "Chair", CategoryId = 4 }
        );

    }
}