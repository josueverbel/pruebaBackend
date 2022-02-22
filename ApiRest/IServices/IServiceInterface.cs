using ApiRest.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ApiRest.IServices
{
    public interface IServiceInterface<T> where T : class
    {
       

        Task<ActionResult<IEnumerable<T>>> GetAllAsync();
        Task<ActionResult<T?>> GetByIdAsync(int Id);
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool?> DeleteAsync(int Id);
       
        
    }
}
