using System;
namespace Microservice.api.Libreria.Core.Entities
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAtribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAtribute(string collectionName)
        {
            CollectionName = collectionName;
        }

    }
}
