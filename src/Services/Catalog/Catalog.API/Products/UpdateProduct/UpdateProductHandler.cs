namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    : ICommand<UpdateProductResult>;

public record UpdateProductResult (bool IsSuccess);

public class UpdateProductQueryHandler(IDocumentSession session, ILogger<UpdateProductQueryHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductQueryHandler.Handle called with {@Query}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }
        
        product.Category = command.Category;
        product.Description = command.Description;
        product.Name = command.Name;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }
}