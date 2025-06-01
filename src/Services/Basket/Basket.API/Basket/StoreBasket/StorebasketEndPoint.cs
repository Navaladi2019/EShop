namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketRequest(ShoppingCart Cart);

    public record StoreBasketResponse(string UserName);
    public class StorebasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
              var response =   await sender.Send(request.Adapt<StoreBasketCommand>());

                return Results.Created($"/basket/{response.UserName}",response.Adapt<StoreBasketResponse>());
            })
              .WithName("StoreBasket")
              .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest)
              .WithSummary("Stores a shopping cart.")
              .WithDescription("This endpoint allows you to store a shopping cart for a user.")
              .WithTags("Basket");
        }
    }
   
}
