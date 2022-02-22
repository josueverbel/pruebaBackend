using ApiRest.Data;
using ApiRest.IServices;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiRest.Services
{
    public class EditorialService : IServiceInterface<Editorial>
    {
        private readonly ApiRestContext _context;

        public EditorialService(ApiRestContext context)
        {
            _context = context;
        }

        public async Task<Editorial> CreateAsync(Editorial entity)
        {
            try
            {
                _context.Editorial.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            } catch (Exception ex)
            {
                return await (Task<Editorial>)Task.FromException(ex);
            }
        }

        public async Task<bool?> DeleteAsync(int Id)
        {
            try
            {
                var editorial = await _context.Editorial.FindAsync(Id);
                if (editorial == null)
                {
                    return null;
                }
                _context.Editorial.Remove(editorial);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return await (Task<bool>)Task.FromException(ex);
            }
        }

        public async Task<ActionResult<IEnumerable<Editorial>>> GetAllAsync()
        {
            try { 
                return await _context.Editorial.ToListAsync();
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<IEnumerable<Editorial>>>)Task.FromException(ex);
            }
        }


        public async Task<ActionResult<Editorial?>> GetByIdAsync(int id)
        {
            try { 
                var editorial = await _context.Editorial.FindAsync(id);
                return editorial;
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<Editorial?>>)Task.FromException(ex);
            }
}

        public async Task<Editorial?> UpdateAsync(Editorial entity)
        {
            try
            {
                var editorial =  _context.Editorial.Find(entity.Id);
                if (editorial == null)
                {
                    return null;
                }
                editorial.Name = entity.Name;
                editorial.Address = entity.Address;
                editorial.Phone = entity.Phone;
                editorial.Email = entity.Email;
                editorial.MaxCount = entity.MaxCount;
                _context.Entry(editorial).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return editorial;
            }
            catch (Exception ex)
            {
                return await (Task<Editorial>)Task.FromException(ex);
            }
        }

        
    }
}
