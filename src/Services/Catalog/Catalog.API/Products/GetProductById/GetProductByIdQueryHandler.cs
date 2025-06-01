namespace Catalog.API.Products.GetProductsById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session) :IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>()
                 .Where(p => p.Id == query.Id)
                 .SingleOrDefaultAsync(cancellationToken);
            return product is null ? throw new ProductNotFoundException(query.Id) : new GetProductByIdResult(product);
        }
    }
   
}
