using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Entities

        /*Inventory bir player account ile bağlantılı olmalı. Ancak bir yaklaşıma göre
        eğer bir ilişkiye ihtiyacın varsa bu domainleri ayırman ya da ayrıldığı yer 
        hatalıdır. Ancak bu örnekte ilişkisellik olması senaryosunu inceleyeceğiz.*/
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string PlayerId { get; set; } = null!;

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; } = null!;

    }
}
