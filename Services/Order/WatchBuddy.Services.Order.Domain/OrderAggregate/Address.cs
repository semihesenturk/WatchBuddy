using WatchBuddy.Services.Order.Domain.Core;

namespace WatchBuddy.Services.Order.Domain.OrderAggregate;

public class Address(string city, string district, string street, string zipCode, string addressDetail)
    : ValueObject
{
    public string City { get; private set; } = city;
    public string District { get; private set; } = district;
    public string Street { get; private set; } = street;
    public string ZipCode { get; private set; } = zipCode;
    public string AddressDetail { get; private set; } = addressDetail;
}