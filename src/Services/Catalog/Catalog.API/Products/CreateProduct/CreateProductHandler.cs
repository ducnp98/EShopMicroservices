using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductResult(Guid Id);

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
    {
        RuleFor(c => c.Name).NotEmpty().WithName("Name is required");
        RuleFor(c => c.Category).NotEmpty().WithName("Category is required");
        RuleFor(c => c.ImageFile).NotEmpty().WithName("ImageFile is required");
        RuleFor(c => c.Price).NotEmpty().WithName("Price is required");
    }
}


internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}