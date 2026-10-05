namespace Catalog.API.Products.GetProducts;

public record GetProductByIdQuery(Guid Id) : IQuery<GerProductByIdResult>;

public record GerProductByIdResult (Product Product);

public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GerProductByIdResult>
{
    public async Task<GerProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", request);

        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        
        return product is null ? throw new ProductNotFoundException(request.Id) : new GerProductByIdResult(product);
    }
}