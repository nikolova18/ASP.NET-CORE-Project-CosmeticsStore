namespace CosmeticsStore.Test.Controllers
{
    using Xunit;
    using System.Linq;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using CosmeticsStore.Controllers;
    using CosmeticsStore.Data.Models;
    using CosmeticsStore.Services.Product.Models;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller
                    .WithData(GetProducts()))
                .ShouldReturn()
                .View(view => view
                   .WithModelOfType<List<LatestProductServiceModel>>()
                   .Passing(m => m.Should().HaveCount(3)));

        private static IEnumerable<Product> GetProducts()
            => Enumerable.Range(0, 10).Select(i => new Product());
    }
}