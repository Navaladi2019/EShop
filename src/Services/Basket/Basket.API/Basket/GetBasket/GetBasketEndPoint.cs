

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var query = new GetBasketQuery(userName);
                var result = await sender.Send(query);
                return Results.Ok(result.Adapt<GetBasketResponse>());
            }).WithDescription("Get Basket by UserName")
              .WithName("GetBasket")
              .Produces<GetBasketResponse>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .WithSummary("Gets a shopping cart by user name.")
              .WithTags("Basket");
        }
    }
    
}