

using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record  StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName cannot be empty");
            RuleFor(x => x.Cart.Items).NotEmpty().WithMessage("Cart must contain at least one item");
        }
    }
    public class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {

            ShoppingCart cart = command.Cart;

            await DeductDiscount(discountProto, cart, cancellationToken);

            await repository.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(cart.UserName);
        }

        private static async Task DeductDiscount(DiscountProtoService.DiscountProtoServiceClient discountProto, ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName ?? string.Empty
                }, cancellationToken: cancellationToken);

                item.Price -= coupon.Amount;
            }
        }
    }
}
