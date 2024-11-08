using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var productsQuery = _context.Products
            .Include(p => p.Category)
            .OrderBy(p => p.ProductId);

        var totalItems = productsQuery.Count();

        var products = productsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = totalItems;

        return View(products);
    }


    public IActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            bool productExists = _context.Products
                .Any(c => c.ProductName.ToLower() == product.ProductName.ToLower());

            if (productExists)
            {
                ModelState.AddModelError("ProductName", $"A product with the name '{product.ProductName}' already exists.");
                ViewBag.Categories = _context.Categories.ToList(); 
                return View(product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(product);
    }

    public IActionResult Edit(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            bool productExists = _context.Products
                .Any(c => c.ProductName.ToLower() == product.ProductName.ToLower());

            if (productExists)
            {
                ModelState.AddModelError("ProductName", $"A product with the name '{product.ProductName}' already exists.");
                ViewBag.Categories = _context.Categories.ToList(); 
                return View(product);
            }

            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();

        }
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}