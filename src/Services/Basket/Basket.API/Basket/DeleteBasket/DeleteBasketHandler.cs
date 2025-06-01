﻿
using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket
{
   public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;


    public record DeleteBasketResult(bool Success);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty");
        }
    }
    public  class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
           await repository.DeleteBasketAsync(request.UserName, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
