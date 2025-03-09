using AutoMapper;
using WatchBuddy.Services.Catalog.API.Dtos;
using WatchBuddy.Services.Catalog.API.Models;

namespace WatchBuddy.Services.Catalog.API.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Watch, WatchDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();
        CreateMap<Watch, WatchCreateDto>().ReverseMap();
        CreateMap<Watch, WatchUpdateDto>().ReverseMap();
    }
}