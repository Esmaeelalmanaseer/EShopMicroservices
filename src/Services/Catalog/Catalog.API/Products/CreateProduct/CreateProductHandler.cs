
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("ImageFile is Required");
    }
}
internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Mapping
        var prodcut = new Product
        {
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Name = command.Name,
        };
        // Save Data Base
        session.Store(prodcut);
        await session.SaveChangesAsync(cancellationToken);
        //return Result
        return new CreateProductResult(prodcut.Id);
    }
}