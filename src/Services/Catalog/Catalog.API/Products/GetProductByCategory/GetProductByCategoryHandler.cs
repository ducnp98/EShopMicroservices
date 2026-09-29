namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<IEnumerable<Product>>;

public record GetProductByCategoryResult (IEnumerable<Product> Products);

public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", request);

        var products = await session.Query<Product>().Where(p => p.Category.Contains(request.Category)).ToListAsync();

        return products;
    }
}