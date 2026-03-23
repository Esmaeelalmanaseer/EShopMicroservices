namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart? Cart);
public class GetBasketQueryHandler(IBasketRepository repositry) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await repositry.GetBasket(request.UserName,cancellationToken);
        return new GetBasketResult(basket);
    }
}
