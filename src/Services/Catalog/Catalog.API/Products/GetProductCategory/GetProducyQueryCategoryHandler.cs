using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductCategory;

public record GetProcutByCategoryQuery(string Category) : IQuery<GetProcutByCategoryResult>;
public record GetProcutByCategoryResult(IEnumerable<Product> Products);
internal class GetProducyQueryCategoryHandler(IDocumentSession session) : IQueryHandler<GetProcutByCategoryQuery, GetProcutByCategoryResult>
{
    public async Task<GetProcutByCategoryResult> Handle(GetProcutByCategoryQuery query, CancellationToken cancellationToken)
    {
        var product = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);
        return new GetProcutByCategoryResult(product);
    }
}
