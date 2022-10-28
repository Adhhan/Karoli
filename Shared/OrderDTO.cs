namespace APIKarolinska.Shared;

public class OrderDTO
{
    public int Id { get; set; }
    public IEnumerable<ProductDTO> Products { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal TotalCost { get; set; }
}
