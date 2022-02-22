using ApiRest.Data;
using ApiRest.IServices;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Services
{
    public class AuthorService : IServiceInterface<Author>
    {
        private readonly ApiRestContext _context;

        public AuthorService(ApiRestContext context)
        {
            _context = context;
        }

        public async Task<Author> CreateAsync(Author entity)
        {
            try
            {
                _context.Author.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return await (Task<Author>)Task.FromException(ex);
            }
        }

        public async Task<bool?> DeleteAsync(int Id)
        {
            try
            {
                var Author = await _context.Author.FindAsync(Id);
                if (Author == null)
                {
                    return null;
                }
                _context.Author.Remove(Author);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return await (Task<bool>)Task.FromException(ex);
            }
        }

        public async Task<ActionResult<IEnumerable<Author>>> GetAllAsync()
        {
            try
            {
                return await _context.Author.ToListAsync();
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<IEnumerable<Author>>>)Task.FromException(ex);
            }
        }


        public async Task<ActionResult<Author?>> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Author.FindAsync(id);
                
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<Author?>>)Task.FromException(ex);
            }
        }

        public async Task<Author?> UpdateAsync(Author entity)
        {
            try
            {
                var Author = _context.Author.Find(entity.Id);
                if (Author == null)
                {
                    return null;
                }
                Author.FirstName = entity.FirstName;
                Author.LastName = entity.LastName;
                Author.Dob = entity.Dob;
                Author.Email = entity.Email;
                Author.City = entity.City;
                _context.Entry(Author).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Author;
            }
            catch (Exception ex)
            {
                return await (Task<Author>)Task.FromException(ex);
            }
        }

        

    }
}
