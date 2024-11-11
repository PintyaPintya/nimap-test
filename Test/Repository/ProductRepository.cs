using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public ProductResponse GetList(int page, int pageSize)
    {
        var product = new ProductResponse();

        var productsQuery = _context.Products
            .Include(p => p.Category)
            .OrderBy(p => p.ProductId);

        var totalItems = productsQuery.Count();

        var products = productsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        product.Products = products;
        product.TotalCount = totalItems;

        return product;
    }

    private bool ProductExists(Product product)
    {
        bool productExists = _context.Products
                .Any(c => c.ProductName.ToLower() == product.ProductName.ToLower() 
                        && c.CategoryId == product.CategoryId);

        return productExists;
    }

    public bool Create(Product product)
    {
        if (ProductExists(product))
        {
            return false;
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        return true;
    }

    public bool Edit(Product product)
    {
        if (ProductExists(product))
        {
            return false;
        }

        _context.Update(product);
        _context.SaveChanges();

        return true;
    }


    public bool Delete(int id)
    {
        var product = _context.Products.Find(id);

        if(product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        
        return true;
    }

}