public interface ICategoryRepository
{
    public List<Category> GetCategoryList(int page, int pageSize);
    public int GetCategoriesCount();
    public bool Create(Category category);
    public Category GetCategory(int id);
    public bool Edit(Category category);
    public void Delete(Category category);
}