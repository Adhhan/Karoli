namespace APIKarolinska.Server.Data;

public class ProductListing
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = "";
    public int Amount { get; set; }
    public TimeSpan DeliveryTime { get; set; }
}