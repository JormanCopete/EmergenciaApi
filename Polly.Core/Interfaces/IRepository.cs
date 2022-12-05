using Polly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polly.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task<T> GetById(string id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task Delete(string id);
    }
}
