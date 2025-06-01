using Catalog.API.Products.CreateProduct;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductsRequest(int? PageNumber = 1,int? PageSize = 10);
    public record GetProductResPonse(IEnumerable<Product> Products);

    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender, [AsParameters] GetProductsRequest request) =>
            {
                var result = await sender.Send(request.Adapt<GetProductsQuery>());
                return Results.Ok(result.Adapt<GetProductResPonse>());
            }).WithName("GetProducts")
            .Produces<GetProductResPonse>(StatusCodes.Status200OK)
            .WithSummary("Gets All Product")
            .WithDescription("This endpoint allows you to get all products in the catalog."); ;
        }
    }
}
