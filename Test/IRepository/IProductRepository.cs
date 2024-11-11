public interface IProductRepository
{
    ProductResponse GetList(int page, int pageSize);
    bool Create(Product product);

    bool Edit(Product product);
    
}