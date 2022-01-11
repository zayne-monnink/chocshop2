namespace ChocShop2.Models;

public class CheckoutSubmitModel
{
    public string FullName { get; set; }
    public string FullAddress { get; set; }
    public List<string> ProductIds { get; set; }
    public List<int> Quantities { get; set; }
    public string CreditCardNumber { get; set; }
    public string CreditCardExpiryYear  { get; set; }
    public string CreditCardExpiryMonth { get; set; }
    public string CreditCardCcv { get; set; }

}
