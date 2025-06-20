﻿using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IPagedList<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("{HandlerName} called with Query {Query}", nameof(GetProductsQueryHandler), query);
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 10,cancellationToken);
            return new GetProductsResult(products);
        }
    }
    
}
