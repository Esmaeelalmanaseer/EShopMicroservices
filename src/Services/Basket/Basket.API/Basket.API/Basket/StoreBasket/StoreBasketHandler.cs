
namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart Can Not Be Null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User Name Is Required");
    }
}
public class StoreBasketCommandHandler(IBasketRepository repository, DiscounPrototService.DiscounPrototServiceClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        await DeductDiscount(request.Cart, cancellationToken);
        await repository.StoreBasket(request.Cart, cancellationToken);
        return new(request.Cart.UserName);
    }
    private async Task DeductDiscount(ShoppingCart item, CancellationToken cancellationToken)
    {
        foreach (var cartItem in item.Items)
        {
            var discount = await discountClient.GetDiscountAsync(new() { ProductName = cartItem.ProductName },cancellationToken: cancellationToken);
            cartItem.Price -= discount.Amount;
        }
    }
}
