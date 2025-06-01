namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(
        string Name,
        List<string> Categories,
        string Description,
        string ImageFile,
        decimal Price
    );

    public record CreateProductResponse(
        Guid Id
    );
    public class CreateNewproductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                return Results
                .Created($"/products/{result.Id}", result.Adapt<CreateProductResponse>());
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDescription("This endpoint allows you to create a new product in the catalog.");
        }
    }
}
