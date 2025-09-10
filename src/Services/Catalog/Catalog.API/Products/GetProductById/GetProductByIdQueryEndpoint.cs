
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductById;
public record GetProductByIdResponse(Product Product);
public class GetProductByIdQueryEndpoint() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("Products/{id:guid}", async (ISender sendder, Guid id) =>
        {
            var result = await sendder.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        }).WithName("GetProductsById")
        .Produces<CreateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id"); ;
    }
}
