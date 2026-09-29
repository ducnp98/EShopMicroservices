namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);
public record UpdateProductsResponse(bool IsSuccess);

public class UpdateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/product", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}