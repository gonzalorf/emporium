using AutoMapper;
using Emporium.Application.Products.Dtos;
using Emporium.Domain.Products;
using Emporium.Domain.Providers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductId, Guid>().ConstructUsing(o => o.Value);
        CreateMap<ProviderId, Guid>().ConstructUsing(o => o.Value);        
        CreateMap<Product, ProductDto>();
    }
}