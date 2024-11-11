public class Category
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}

public class CategoryResponse
{
    public List<Category>? Categories { get; set; }
    public int TotalCount { get; set; }

}