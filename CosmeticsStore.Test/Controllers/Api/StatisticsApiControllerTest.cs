namespace CosmeticsStore.Test.Controllers.Api
{
    using CosmeticsStore.Controllers.Api;
    using CosmeticsStore.Test.Mocks;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            // Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            // Act
            var result = statisticsController.GetStatistics();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalProducts);
            Assert.Equal(10, result.TotalPurchase);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}
