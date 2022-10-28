namespace APIKarolinska.Server.Data;

public class Inventory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public TimeSpan DeliveryTime { get; set; }
}