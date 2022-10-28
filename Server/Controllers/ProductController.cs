using APIKarolinska.Server.Data;
using APIKarolinska.Shared;
using Microsoft.AspNetCore.Mvc;

namespace APIKarolinska.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly DBContext context;

    public ProductController(DBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<ProductDTO> Get()
    {
        return context.Products.Select(x => new ProductDTO() { Id = x.Id, Name = x.Name, Price = x.Price });
    }
}