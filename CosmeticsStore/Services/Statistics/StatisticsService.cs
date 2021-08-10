namespace CosmeticsStore.Services.Statistics
{
    using CosmeticsStore.Data;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalProducts = this.data.Products.Count(c => c.IsPublic);
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalProducts = totalProducts,
                TotalUsers = totalUsers
            };
        }
    }
}

