using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    :ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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

        //return Result
        return new CreateProductResult(Guid.NewGuid());
    }
}
