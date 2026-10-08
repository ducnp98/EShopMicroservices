using Mapster;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest (ShoppingCart Cart);

public record StoreBasketResponse(string Username);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = new StoreBasketRequest(request.Cart);
            var result = await sender.Send(command);
            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.Username}", response);
        })
        .WithName("StoreShoppingCart")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store shopping cart")
        .WithDescription("Store shopping cart");
    }
}