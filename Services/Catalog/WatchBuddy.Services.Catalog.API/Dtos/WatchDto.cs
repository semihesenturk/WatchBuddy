namespace WatchBuddy.Services.Catalog.API.Dtos;

public class WatchDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string UserId { get; set; }
    public string Image { get; set; }
    public DateTime CreatedTime { get; set; }
    public FeatureDto Feature { get; set; }
    public string CategoryId { get; set; }
    public CategoryDto Category { get; set; }
}