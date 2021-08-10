namespace CosmeticsStore.Infrastructure
{
    using AutoMapper;
    using CosmeticsStore.Data.Models;
    using CosmeticsStore.Models.Products;
    using CosmeticsStore.Services.Product.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, ProductCategoryServiceModel>();

            this.CreateMap<Product, LatestProductServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();

            this.CreateMap<Product, ProductServiceModel>()
              .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));

            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId))
                .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));
        }
    }
}
