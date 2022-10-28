using System.Data.Common;
using APIKarolinska.Server.Models;
using APIKarolinska.Server.Data;
namespace Tests;

public class UnitTest1
{
    [Fact]
    public void ValidOrderShouldBeTrue()
    {
        var context = new DBContext();
        var order = new Order(context);

        order.Products.Add(new Product() { ProductId = 1, Amount = 5 }); 
        var result = order.PlaceOrder(); 
        Assert.True(result.success, "Should be true");
        Assert.False(result.message.Length > 0, "Message should be empty.");
    }

    [Fact]
    public void InvalidProductIDShouldBeFalse()
    {
        var context = new DBContext();
        var order = new Order(context);

        order.Products.Add(new Product() { ProductId = 12412, Amount = 5 }); 
        var result = order.PlaceOrder(); 
        Assert.False(result.success, "Should be false");
        Assert.True(result.message.Length > 0, "Message should not be empty.");
    }

    [Fact]
    public void EmptyProductShouldBeFalse()
    {
        var context = new DBContext();
        var order = new Order(context);

        var result = order.PlaceOrder(); 

        Assert.False(result.success, "Should be false");
        Assert.True(result.message.Length > 0, "Message should not be empty.");
    }

    [Fact]
    public void NegativeAmountShouldBeFalse()
    {
        var context = new DBContext();
        var order = new Order(context);

        order.Products.Add(new Product() { ProductId = 1, Amount = -5 }); 
        var result = order.PlaceOrder(); 

        Assert.False(result.success, "Should be false");
        Assert.True(result.message.Length > 0, "Message should not be empty.");
    }
}