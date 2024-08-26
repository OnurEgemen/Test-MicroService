namespace InventoryService.API.Dtos
{
    public record ItemCreateDto(string Name);
    public record ItemUpdateDto(string id, string Name);
}
