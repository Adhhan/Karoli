namespace APIKarolinska.Server.Data;

public class DBContext
{
    public List<ProductListing> Products { get; set; }
    public List<Inventory> Inventories { get; set; }
    public List<OrderEntry> Orders { get; set; } = new();

    public DBContext()
    {
        Inventories = new() {
            new Inventory() {Id = 1, ProductId = 1, Amount = 15, DeliveryTime = TimeSpan.FromDays(2)},
            new Inventory() {Id = 2, ProductId = 1, Amount = 120, DeliveryTime = TimeSpan.FromDays(2)},
            new Inventory() {Id = 3, ProductId = 2, Amount = 10, DeliveryTime = TimeSpan.FromDays(2)},
            new Inventory() {Id = 4, ProductId = 3, Amount = 50, DeliveryTime = TimeSpan.FromDays(2)},
            new Inventory() {Id = 5, ProductId = 3, Amount = 10, DeliveryTime = TimeSpan.FromDays(2)}
        };
        Products = new() {
            new ProductListing(){ Id = 1, Name = "Godis",  Price = 10.2m},
            new ProductListing(){ Id = 2, Name = "Kol",  Price = 1.6m},
            new ProductListing(){ Id = 3, Name = "Chips",  Price = 20m},
        };
    }
}