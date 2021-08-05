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
            this.CreateMap<Product, LatestProductServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();

            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
