namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndPoint : ICarterModule
    {

        public record DeleteProductResponse(bool IsSuccess);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id), cancellationToken);
                return Results.Ok(result.Adapt<DeleteProductResponse>());
            })
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Deletes Product")
            .WithDescription("This endpoint allows you to delete a product by its id.");
            ;
        }
    }
    
}
