using APIKarolinska.Shared;
using Microsoft.AspNetCore.Components;

namespace APIKarolinska.Client.Pages
{
    public class OrderBase : ComponentBase
    {
        [Parameter]
        public OrderDTO Order { get; set; } = new() {Products = new List<ProductDTO>()};
    }
}