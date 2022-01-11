namespace ChocShop2.DAL.Entities;

public class Order_OrderLines
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; }

}