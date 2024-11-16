public interface IProductRepository
{
    public List<Product> GetProductList(int page, int pageSize);
    public int GetProductCount();
    public List<Category> GetCategoryList();
    public bool Create(Product product);
    public Product GetProduct(int id);
    public bool Edit(Product product);
    public void Delete(Product product);
}