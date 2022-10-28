namespace APIKarolinska.Server.Data;

public class OrderEntry
{
    public int Id { get; set; }
    public List<ProductListing> Products { get; set; } = new();
    public decimal TotalCost { get; set; }
    public DateTime DeliveryDate { get; set; }
}