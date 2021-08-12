namespace CosmeticsStore.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using CosmeticsStore.Controllers;
    using CosmeticsStore.Services.Product.Models;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.Products;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
             => MyController<HomeController>
                .Instance(controller => controller
                    .WithData(TenPublicCars))
                .Calling(c => c.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestProductsCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                        .WithValueOfType<List<LatestProductServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                   .WithModelOfType<List<LatestProductServiceModel>>()
                    .Passing(model => model.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}