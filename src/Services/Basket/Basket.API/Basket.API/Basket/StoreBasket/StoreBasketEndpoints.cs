namespace Basket.API.Basket.StoreBasket;
public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender mediator) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await mediator.Send(command);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.UserName}", response);
        }).WithName("CreateProdcut")
          .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Create Product")
           .WithDescription("Create Product");
    }
}
