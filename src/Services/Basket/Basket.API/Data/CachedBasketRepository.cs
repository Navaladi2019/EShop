
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository,IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
             await cache.RemoveAsync(userName, cancellationToken);

            return await repository.DeleteBasketAsync(userName, cancellationToken);

        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {

            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrWhiteSpace(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            }
            var basket =  await repository.GetBasketAsync(userName, cancellationToken);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            }, cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(cart, cancellationToken);

            await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            }, cancellationToken);


            return cart;
        }
    }
}
