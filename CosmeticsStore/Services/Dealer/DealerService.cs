namespace CosmeticsStore.Services.Dealer
{
    using System.Linq;
    using CosmeticsStore.Data;

    public class DealerService : IDealerService
    {
        private readonly ApplicationDbContext data;

        public DealerService(ApplicationDbContext data)
            =>this.data = data;

        public int IdByUser(string userId) 
            => this.data
                .Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public bool IsDealer(string userId)
            => this.data
                .Dealers
                .Any(d => d.UserId == userId);
    }
}
