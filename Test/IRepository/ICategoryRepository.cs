public interface ICategoryRepository
{
    CategoryResponse GetList(int page, int pageSize);
    bool Create(Category category);
    bool Edit(Category category);
    bool Delete(int id);
}