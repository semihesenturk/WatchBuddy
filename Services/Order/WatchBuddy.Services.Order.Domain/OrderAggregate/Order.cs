using WatchBuddy.Services.Order.Domain.Core;

namespace WatchBuddy.Services.Order.Domain.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public string BuyerId { get; private set; }
    
    public Address Address { get; private set; }

    private readonly List<OrderItem> _orderItems = [];

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    
    public Order(string buyerId, Address address)
    {
        BuyerId = buyerId;
        Address = address;
        CreatedOn = DateTime.UtcNow;
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        var exitsProduct = _orderItems.Any(x => x.ProductId == orderItem.ProductId);
        if(!exitsProduct)
            _orderItems.Add(orderItem);
        
    }
    
    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
}