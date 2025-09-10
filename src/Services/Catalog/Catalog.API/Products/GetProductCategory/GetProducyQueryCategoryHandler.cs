using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductCategory;

public record GetProcutByCategoryQuery(string Category) : IQuery<GetProcutByCategoryResult>;
public record GetProcutByCategoryResult(IEnumerable<Product> Products);
internal class GetProducyQueryCategoryHandler(IDocumentSession session, ILogger<GetProducyQueryCategoryHandler> logger) : IQueryHandler<GetProcutByCategoryQuery, GetProcutByCategoryResult>
{
    public async Task<GetProcutByCategoryResult> Handle(GetProcutByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProcutByCategoryResult.Handle called with {@Query}", query);
        var product = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);
        return new GetProcutByCategoryResult(product);
    }
}
