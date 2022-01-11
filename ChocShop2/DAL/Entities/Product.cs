namespace ChocShop2.DAL.Entities;

public class Product: RavenDbEntity
{
    
    public string ProductName { get; set; }
    public decimal Price { get; set; }
}