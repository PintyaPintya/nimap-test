using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Product> GetProductList(int page, int pageSize)
    {
        var productsQuery = _context.Products
        .OrderBy(p => p.ProductId)
        .Include(p => p.Category);

        var products = productsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return products;
    }

    public int GetProductCount()
    {
        int count = _context.Products.Count();
        return count;
    }

    public List<Category> GetCategoryList()
    {
        var categories = _context.Categories.ToList();
        return categories;
    }

    public bool Create(Product product)
    {
        if(ProductExists(product)) return false;        

        _context.Products.Add(product);
        _context.SaveChanges();
        return true;
    }

    public Product GetProduct(int id)
    {
        var product = _context.Products.Find(id);

        if(product == null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        return product;
    }

    public bool Edit(Product product)
    {
        if(ProductExists(product)) return false;

        _context.Products.Update(product);
        _context.SaveChanges();
        return true;
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    private bool ProductExists(Product product)
    {
        bool productExists = _context.Products
            .Any(p => p.ProductName.ToLower() == product.ProductName.ToLower() && p.CategoryId == product.CategoryId);
        
        return productExists;
    }
}