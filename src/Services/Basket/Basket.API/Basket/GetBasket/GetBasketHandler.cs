namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;
public record  GetBasketResult(ShoppingCart cart);

public class GetBasketHandler: IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        return new GetBasketResult(new ShoppingCart("smw"));
    }
}