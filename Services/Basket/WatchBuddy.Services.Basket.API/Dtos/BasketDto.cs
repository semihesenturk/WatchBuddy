namespace WatchBuddy.Services.Basket.API.Dtos;

public class BasketDto
{
    public string UserId { get; set; }
    public string DiscountCode { get; set; }
    public List<BasketItemDto> Items { get; set; }

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}