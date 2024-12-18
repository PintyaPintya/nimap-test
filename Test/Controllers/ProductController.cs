using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IProductRepository _productRepository;

    public ProductController(ApplicationDbContext context, IProductRepository productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var response = _productRepository.GetList(page, pageSize);

        int totalPages = (int)Math.Ceiling((double)response.TotalCount / pageSize);

        if (page < 1)
        {
            page = 1;
        }
        else if (page > totalPages)
        {
            page = totalPages;
            response = _productRepository.GetList(page, pageSize);
        }

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = totalPages;

        return View(response.Products);
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
            bool response = _productRepository.Create(product);

            if (!response)
            {
                ModelState.AddModelError("ProductName", $"A product with the name '{product.ProductName}' already exists.");
                ViewBag.Categories = _context.Categories.ToList();
                return View(product);
            }
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
            bool response = _productRepository.Edit(product);

            if (!response)
            {
                ModelState.AddModelError("ProductName", $"A product with the name '{product.ProductName}' already exists.");
                ViewBag.Categories = _context.Categories.ToList();
                return View(product);
            }

            return RedirectToAction("Index");
        }

        ViewBag.Categories = _context.Categories.ToList();
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        var product = _context.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        bool success = _productRepository.Delete(id);

        if (!success)
        {
            return NotFound();
        }        

        return RedirectToAction("Index");
    }
}