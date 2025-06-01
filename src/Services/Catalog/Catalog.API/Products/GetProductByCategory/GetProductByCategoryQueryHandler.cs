
namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
         return  await session.Query<Product>()
                .Where(p => p.Category.Contains(request.Category))
                .ToListAsync(cancellationToken)
                .ContinueWith(t => new GetProductByCategoryResult(t.Result), cancellationToken);
        }
    }
}
// Fetch only required
// Do only whats needed