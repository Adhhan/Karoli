using APIKarolinska.Server.Data;
using APIKarolinska.Shared;
using Microsoft.AspNetCore.Mvc;

namespace APIKarolinska.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly DBContext context;

    public InventoryController(DBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<ProductDTO> Get()
    {
        return from inventory in context.Inventories
                join product in context.Products on inventory.ProductId equals product.Id
                select new ProductDTO() { Id = product.Id, Amount = inventory.Amount, Name = product.Name, Price = product.Price };
    }
}