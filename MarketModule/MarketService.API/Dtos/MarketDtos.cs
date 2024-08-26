namespace MarketService.API.Dtos
{
    public record CreateMarketDto(string ItemId, string InventoryId,
        decimal price, string PlayerId);
}
