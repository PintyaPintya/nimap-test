public class Product
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}