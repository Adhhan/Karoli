using APIKarolinska.Server.Data;
using APIKarolinska.Server.Models;
using APIKarolinska.Shared;
using Microsoft.AspNetCore.Mvc;

namespace APIKarolinska.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly DBContext context;

    public OrderController(DBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<OrderDTO> Get()
    {
        return context
           .Orders
           .Select(x => new OrderDTO()
           {
               Id = x.Id,
               TotalCost = x.TotalCost,
               DeliveryDate = x.DeliveryDate,
               Products = x.Products.Select(y => new ProductDTO() { Id = y.Id, Name = y.Name, Price = y.Price })
           });
    }

    [HttpPost]
    public IActionResult PlaceOrder([FromBody] OrderDTO orderDTO)
    {
        if(!orderDTO.Products.Any())
            return BadRequest("Empty order.");

        var products = from product in context.Products
                       join orderProduct in orderDTO.Products on product.Id equals orderProduct.Id
                       select new Product() { ProductId = product.Id, Amount = orderProduct.Amount, Price = product.Price };

        var order = new Order(context) { Products = products.ToList() };

        var (success, message) = order.PlaceOrder();

        if (success)
        {
            return Ok();
        }
        else
        {
            return BadRequest(message);
        }
    }
}