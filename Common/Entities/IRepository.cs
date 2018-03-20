using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        bool Exists(int id);
        void Add(T t);
        void Delete(T t);
        bool Save();
    }
}
