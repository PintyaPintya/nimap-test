using Microsoft.AspNetCore.Mvc;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly int pageSize = 10;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index(int page = 1)
    {
        int totalCategories = _categoryRepository.GetCategoriesCount();
        
        int totalPages = (int)Math.Ceiling((double)totalCategories / pageSize);

        if(page < 1)
        {
            page = 1;
        }
        else if(page > totalPages)
        {
            page = totalPages;
        }

        var categories = _categoryRepository.GetCategoryList(page,pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = totalPages;

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
            bool response = _categoryRepository.Create(category);

            if (!response)
            {
                ModelState.AddModelError("CategoryName", $"A Category with the name '{category.CategoryName}' already exists.");
                return View(category);
            }

            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Edit(int id)
    {
        try
        {
            var category = _categoryRepository.GetCategory(id);
            return View(category);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            bool response = _categoryRepository.Edit(category);

            if(!response)
            {
                ModelState.AddModelError("CategoryName", $"A Category with the name '{category.CategoryName}' already exists.");
                return View(category);
            }
            
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            var category = _categoryRepository.GetCategory(id);
            return View(category);
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(Category category)
    {
        _categoryRepository.Delete(category);
        return RedirectToAction("Index");
    }
}