
using Catalog.API.Exceptions;
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductCategory;
public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProducyQueryByCategoryEndpointcs : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("product/{category}", async (ISender sender, string category) =>
        {
            var result = await sender.Send(new GetProcutByCategoryQuery(category));
            if (result is null)
                throw new ProductNotFoundException();
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        }).WithName("GetProductsByCategory")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category"); ;
    }
}
