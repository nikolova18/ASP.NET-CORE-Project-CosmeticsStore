namespace CosmeticsStore.Services.Dealer
{
    public interface IDealerService
    {
        public bool IsDealer(string userId);

        public int IdByUser(string userId);
    }
}
