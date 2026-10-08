namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRespones(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            var response = result.Adapt<DeleteBasketRespones>();
            return Results.Ok(response);
        }).WithName("Delete shopping-basket")
        .Produces<DeleteBasketRespones>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete shopping-basket")
        .WithDescription("Delete shopping-basket");
    }
}