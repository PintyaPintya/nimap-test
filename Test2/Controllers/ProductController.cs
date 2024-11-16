using Microsoft.AspNetCore.Mvc;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly int pageSize = 10;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IActionResult Index(int page = 1)
    {
        int totalProducts = _productRepository.GetProductCount();
        int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

        if (page < 1)
        {
            page = 1;
        }
        else if (page > totalPages)
        {
            page = totalPages;
        }

        var products = _productRepository.GetProductList(page, pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = totalPages;

        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _productRepository.GetCategoryList();
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
                ViewBag.Categories = _productRepository.GetCategoryList();
                return View(product);
            }

            return RedirectToAction("Index");
        }

        return View(product);
    }

    public IActionResult Edit(int id)
    {
        try
        {
            var product = _productRepository.GetProduct(id);
            ViewBag.Categories = _productRepository.GetCategoryList();
            return View(product);
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if(ModelState.IsValid)
        {
            bool response = _productRepository.Edit(product);

            if(!response)
            {
                ModelState.AddModelError("ProductName", $"A product with the name '{product.ProductName}' already exists.");
                ViewBag.Categories = _productRepository.GetCategoryList();
                return View(product);
            }
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            var product = _productRepository.GetProduct(id);
            return View(product);
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(Product product)
    {
        _productRepository.Delete(product);
        return RedirectToAction("Index");
    }
}