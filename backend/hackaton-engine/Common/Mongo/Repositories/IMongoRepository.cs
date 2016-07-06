using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Models;

namespace Common.Mongo.Repositories
{
    public interface IMongoRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Add(T item);
        void Add(IEnumerable<T> item);

        bool Remove(int id);
        bool Remove(Expression<Func<T, bool>> delExpression);
    }
}