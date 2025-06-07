
namespace Basket.API.Basket.DeleteBasket
{

    public record DeleteBasketResponse(bool Success);
    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userName);
                var result = await sender.Send(command, CancellationToken.None);
                return result.Success ? Results.Ok(new DeleteBasketResponse(true)) : Results.BadRequest(new DeleteBasketResponse(false));
            })
            .WithName("DeleteBasket")
            .WithSummary("Deletes a user's shopping basket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .Produces<DeleteBasketResponse>(StatusCodes.Status400BadRequest);
        }
    }
  
}
