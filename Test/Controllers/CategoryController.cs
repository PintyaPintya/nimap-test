using Microsoft.AspNetCore.Mvc;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // public IActionResult Index()
    // {
    //     var categories = _context.Categories.ToList();
    //     return View(categories);
    // }

    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var categoriesQuery = _context.Categories
            .OrderBy(p => p.CategoryId);

        var totalCategories = categoriesQuery.Count();

        var categories = categoriesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = totalCategories;

        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            bool categoryExists = _context.Categories
                .Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower());

            if (categoryExists)
            {
                ModelState.AddModelError("CategoryName", $"A category with the name '{category.CategoryName}' already exists.");
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Edit(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            bool categoryExists = _context.Categories
                .Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower());

            if (categoryExists)
            {
                ModelState.AddModelError("CategoryName", $"A category with the name '{category.CategoryName}' already exists.");
                return View(category);
            }
            
            _context.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}