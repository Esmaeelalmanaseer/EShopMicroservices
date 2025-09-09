
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProduct;
public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }).WithName("GetProducts")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}
