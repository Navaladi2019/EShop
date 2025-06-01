namespace Catalog.API.Products.GetProductsById
{

    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    var result = await sender.Send(new GetProductByIdQuery(id));
                    return Results.Ok(result.Adapt<GetProductByIdResponse>());
                }catch(ProductNotFoundException)
                {
                    return Results.NotFound();
                }

            }).WithName("GetProductById")
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Gets a Product by Id")
            .WithDescription("This endpoint allows you to get a product by its Id.");
        }
    }
    
}
