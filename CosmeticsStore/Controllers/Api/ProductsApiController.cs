﻿namespace CosmeticsStore.Controllers.Api
{
    using CosmeticsStore.Models.Api.Products;
    using CosmeticsStore.Services.Product;
    using CosmeticsStore.Services.Product.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService products;

        public ProductsApiController(IProductService products)
            => this.products = products;

        [HttpGet]
        public ProductQueryServiceModel All([FromQuery] AllProductsApiRequestModel query)
            => this.products.All(
                query.Brand,
                query.CategoryName,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.ProductsPerPage);
    }
}
