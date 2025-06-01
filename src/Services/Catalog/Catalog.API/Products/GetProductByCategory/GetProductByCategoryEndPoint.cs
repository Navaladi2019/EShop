namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProductByCategoryQuery(category);
                var result = await sender.Send(query, cancellationToken);
                return Results.Ok(result.Adapt<GetProductByCategoryResponse>());
            }).WithName("GetProductByCategory")
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Gets a Product by Category")
            .WithDescription("This endpoint allows you to get a product by its Category."); ;
        }
    }
}
