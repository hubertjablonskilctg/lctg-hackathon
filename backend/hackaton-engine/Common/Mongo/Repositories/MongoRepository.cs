using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace Common.Mongo.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : Entity
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find((x) => true).ToList();
        }
        public T Get(int id)
        {
            return _collection.Find((x) => x.Id == id).FirstOrDefault();
        }

        public void Add(T item)
        {
            _collection.InsertOne(item);
        }
        public void Add(IEnumerable<T> items)
        {
            _collection.InsertMany(items);
        }

        public bool Remove(int id)
        {
            return _collection.DeleteOne((x) => x.Id == id).DeletedCount == 1;
        }
        public bool Remove(Expression<Func<T,bool>> delExpression)
        {
            return _collection.DeleteMany(delExpression).IsAcknowledged;
        }

        public bool Update(int id, T item)
        {
            Remove(id);
            Add(item);

            return true;
        }

        public int GetHighestId()
        {
            var records = GetAll();

            if (!records.Any()) return 0;

            return GetAll().Max(o => o.Id);
        }
    }
}