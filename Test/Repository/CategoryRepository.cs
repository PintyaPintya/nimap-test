public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public CategoryResponse GetList(int page, int pageSize)
    {
        var category = new CategoryResponse();

        var categoriesQuery = _context.Categories
            .OrderBy(p => p.CategoryId);

        var totalCategories = categoriesQuery.Count();

        var categories = categoriesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        category.Categories = categories;
        category.TotalCount = totalCategories;

        return category;
    }

    private bool CategoryExists(Category category)
    {
        bool categoryExists = _context.Categories
            .Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower());

        return categoryExists;
    }

    public bool Create(Category category)
    {
        if (CategoryExists(category))
        {
            return false;
        }

        _context.Categories.Add(category);
        _context.SaveChanges();

        return true;
    }

    public bool Edit(Category category)
    {
        if (CategoryExists(category))
        {
            return false;
        }

        _context.Update(category);
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        var category = _context.Categories.Find(id);

        if(category == null)        
        {
            return false;
        }
        
        _context.Categories.Remove(category);
        _context.SaveChanges();
        
        return true;
    }
}