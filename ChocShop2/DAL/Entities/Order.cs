namespace ChocShop2.DAL.Entities;

public class Order : RavenDbEntity
{
    public string OrderNumber { get; set; }
    public DateTime DateCreated { get; set; }
    public decimal OverallTotal { get; set; }
    public int StatusId { get; set; } // 1=New, 2=Shipped

    public List<Order_OrderLines> OrderLines { get; set; }

}