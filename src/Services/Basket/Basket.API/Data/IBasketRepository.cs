namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasketAsync(string userName,CancellationToken cancellationToken = default);

        public Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
        public Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    }
}
