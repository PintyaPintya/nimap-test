using Microsoft.AspNetCore.Mvc;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ApplicationDbContext context, ICategoryRepository categoryRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var response = _categoryRepository.GetList(page, pageSize);

        int totalPages = (int)Math.Ceiling((double)response.TotalCount / pageSize);

        if (page < 1)
        {
            page = 1;
        }
        else if (page > totalPages)
        {
            page = totalPages;
            response = _categoryRepository.GetList(page, pageSize);
        }

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = totalPages;

        return View(response.Categories);
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
            bool response = _categoryRepository.Create(category);

            if (!response)
            {
                ModelState.AddModelError("CategoryName", $"A category with the name '{category.CategoryName}' already exists.");
                return View(category);
            }

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
            bool response = _categoryRepository.Edit(category);

            if (!response)
            {
                ModelState.AddModelError("CategoryName", $"A category with the name '{category.CategoryName}' already exists.");
                return View(category);
            }
            
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
        bool success = _categoryRepository.Delete(id);

        if (!success)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }
}