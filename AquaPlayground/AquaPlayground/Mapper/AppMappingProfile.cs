namespace AquaPlayground.Mapper
{
    using AquaPlayground.Models;
    using AquaPlayground.ViewModels;
    using AutoMapper;

    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ProductPostViewModel, Product>()
                .ForMember(x => x.ProductTags, y => y.MapFrom(z => z.TagsIds.Select(n => new ProductToTag() { TagId = n }).ToList()));

            CreateMap<ProductPostViewModel, Nutrition>();

            CreateMap<RegisterViewModel, User>();

            CreateMap<Product, ProductGetViewModel>();

            CreateMap<CartProduct, OrderProduct>();
        }
    }
}
