public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Category> GetCategoryList(int page, int pageSize)
    {
        var categoriesQuery = _context.Categories.OrderBy(p => p.CategoryId);

        var categories = categoriesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return categories;
    }

    public int GetCategoriesCount()
    {
        int count = _context.Categories.Count();
        return count;
    }

    public bool Create(Category category)
    {
        if(CategoryExists(category)) return false;

        _context.Categories.Add(category);
        _context.SaveChanges();
        return true;
    }

    public Category GetCategory(int id)
    {
        var category = _context.Categories.Find(id);

        if(category == null)
        {
            throw new KeyNotFoundException("Category not found");
        }

        return category;
    }

    public bool Edit(Category category)
    {
        if(CategoryExists(category)) return false;

        _context.Categories.Update(category);
        _context.SaveChanges();
        return true;
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

    private bool CategoryExists(Category category)
    {
        bool categoryExists = _context.Categories.Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower());
        return categoryExists;
    }
}