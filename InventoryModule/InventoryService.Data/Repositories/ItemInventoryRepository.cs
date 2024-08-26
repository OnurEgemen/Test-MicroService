using InventoryService.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Repositories
{
    public class ItemInventoryRepository
    {
        private readonly IMongoCollection<ItemInventory> itemInventoryCollection;
        public ItemInventoryRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("IventoryDb");
            itemInventoryCollection = database.GetCollection<ItemInventory>("itemInventories");
        }

        public async Task<List<ItemInventory>?> GetAll()
        {
            var filter = Builders<ItemInventory>.Filter.Empty;

            var result = await itemInventoryCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<ItemInventory> GetItemInventory(string itemId,string inventoryId)
        {
            var filter1 = Builders<ItemInventory>.Filter.Eq(x => x.ItemId, itemId);
            var filter2 = Builders<ItemInventory>.Filter.Eq(x => x.InventoryId, inventoryId);

             var filter = Builders<ItemInventory>.Filter.And(filter1,filter2);

            var result = await itemInventoryCollection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<ItemInventory> Create(ItemInventory itemInventory)
        {
            await itemInventoryCollection.InsertOneAsync(itemInventory);
            return itemInventory;
        }

        public async Task Update(ItemInventory updatedItemInventory)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, updatedItemInventory.Id);

            await itemInventoryCollection.FindOneAndReplaceAsync(filter, updatedItemInventory);

        }

        public async Task Remove(string id)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, id);

            await itemInventoryCollection.DeleteOneAsync(filter);

        }
    }
}
