using APIKarolinska.Server.Data;

namespace APIKarolinska.Server.Models;

public class Product
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = "";
    public int Amount { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    internal bool CheckInventory(DBContext context)
        => context.Inventories.Where(x => ProductId == x.ProductId).Sum(x => x.Amount) >= Amount;

    internal void CalculateDeliveryTime(DBContext context)
    {
        var productInventories = context
            .Inventories
            .Where(x => x.ProductId == ProductId)
            .OrderBy(x => x.DeliveryTime);

        var total = Amount;
        foreach (var inventory in productInventories)
        {
            total -= inventory.Amount;
            if (total <= 0)
            {
                DeliveryTime = inventory.DeliveryTime;
                break;
            }
        }
    }

    internal void SubtractInventory(DBContext context)
    {
        var productInventory = context
            .Inventories
            .Where(x => x.ProductId == ProductId)
            .OrderBy(x => x.DeliveryTime);

        var amount = Amount;
        foreach (var inventory in productInventory)
        {
            Console.WriteLine($"{amount} {inventory.ProductId}");
            if (inventory.Amount <= amount)
            {
                amount -= inventory.Amount;
                context.Inventories.Remove(inventory);
            }
            if (inventory.Amount >= amount)
            {
                inventory.Amount -= amount;
                amount = 0;
                break;
            }
        }
    }
}