namespace Shared.Interfaces
{
    public interface MarketCreated
    {
        public string ItemId { get; set; }
        public string InventoryId { get; set; }
        public int Count { get; set; }
    }
}
