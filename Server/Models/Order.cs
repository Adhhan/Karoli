using APIKarolinska.Server.Data;

namespace APIKarolinska.Server.Models;

public class Order
{
    private readonly DBContext context;

    public Order(DBContext context)
    {
        this.context = context;
    }
    public int Id { get; set; }
    public List<Product> Products { get; set; } = new();
    public decimal TotalCost { get; set; }
    public DateTime DeliveryDate { get; set; }
    public void CalculateTotalCost() => TotalCost = Products.Sum(x => x.Amount * x.Price);

    private void CalculateDeliveryDate()
    {
        if(!CheckInventory())
            return;

        Products.ForEach(x => x.CalculateDeliveryTime(context));
        DeliveryDate = DateTime.Now.Add(
            Products
            .OrderByDescending(x => x.DeliveryTime)
            .Select(x => x.DeliveryTime)
            .First());
    }

    private bool CheckInventory() => Products.TrueForAll(x => x.CheckInventory(context));

    private bool CheckIfProductsExists()
    {
        var validProducts = context.Products.Select(x => x.Id);
        return !Products.Select(x => x.ProductId).Except(validProducts).Any();
    }

    public (bool success, string message) PlaceOrder()
    {
        var (valid, message) = ValidateOrder();

        if(!valid)
            return (false, message);

        CalculateDeliveryDate();
        CalculateTotalCost();
        var productListings = from productListing in context.Products
                                join product in Products on productListing.Id equals product.ProductId
                                select productListing;
        Products.ForEach(x => x.SubtractInventory(context));

        var nextId = 1;
        if(context.Orders.Count == 0)
            nextId = 1;
        else
            nextId = context.Orders.Max(x => x.Id) + 1;

        context.Orders.Add(new OrderEntry() { Id = nextId, DeliveryDate = DeliveryDate, Products = productListings.ToList(), TotalCost = TotalCost });

        return (true, "");
    }

    private (bool valid,string message) ValidateOrder()
    {
        if(ProductsIsNullOrEmpty())
            return (false, "Order products is empty.");
        if (!CheckIfProductsExists())
            return (false, "Product does not exist.");
        if(!CheckInventory())
            return (false, "Out of stock.");
        if(!CheckProductAmount())
            return (false, "Negative or zero amount.");

        return (true, "");
    }

    private bool ProductsIsNullOrEmpty() => Products is null || Products.Count == 0;
    private bool CheckProductAmount() => Products.TrueForAll(x => x.Amount > 0);
}