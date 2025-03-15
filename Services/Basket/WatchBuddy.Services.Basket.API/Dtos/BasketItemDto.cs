namespace WatchBuddy.Services.Basket.API.Dtos;

public class BasketItemDto
{
    public int Quantity { get; set; }
    public string WatchId { get; set; }
    public string WatchName { get; set; }
    public decimal Price { get; set; }
}