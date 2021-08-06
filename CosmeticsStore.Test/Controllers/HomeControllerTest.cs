namespace CosmeticsStore.Test.Controllers
{
    using CosmeticsStore.Controllers;
    using CosmeticsStore.Data.Models;
    using CosmeticsStore.Services.Product;
    using CosmeticsStore.Services.Statistics;
    using CosmeticsStore.Test.Mocks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

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
                    .WithModelOfType<IndexViewModel>()
                    .Passing(m => m.Products.Should().HaveCount(3)));

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var products = Enumerable
                .Range(0, 10)
                .Select(i => new Product());

            data.Products.AddRange(products);
            data.Users.Add(new User());

            data.SaveChanges();

            var productService = new ProductService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(productService, statisticsService);

            // Act
            var result = homeController.Index();

            // Assert
            // Assert.NotNull(result);

            // var viewResult = Assert.IsType<ViewResult>(result);

            // var model = viewResult.Model;

            // var indexViewModel = Assert.IsType<IndexViewModel>(model);

            // Assert.Equal(3, indexViewModel.Products.Count);
            // Assert.Equal(10, indexViewModel.TotalProducts);
            // Assert.Equal(1, indexViewModel.TotalUsers);

            result
                .Should()
                .NotBeNull()
                .And
                .BeAssignableTo<ViewResult>()
                .Which
                .Model
                .As<IndexViewModel>()
                .Invoking(model =>
                {
                    model.Products.Should().HaveCount(3);
                    model.TotalProducts.Should().Be(10);
                    model.TotalUsers.Should().Be(1);
                })
                .Invoke();
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(
                null,
                null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        private static IEnumerable<Product> GetProducts()
            => Enumerable.Range(0, 10).Select(i => new Product());
    }
}
