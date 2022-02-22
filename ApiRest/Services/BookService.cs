using ApiRest.Data;
using ApiRest.IServices;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Services
{
    public class BookService : IServiceInterface<Book>
    {
        private readonly ApiRestContext _context;

        public BookService(ApiRestContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book entity)
        {
            try
            {
                _context.Book.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return await (Task<Book>)Task.FromException(ex);
            }
        }

        public async Task<bool?> DeleteAsync(int Id)
        {
            try
            {
                var Book = await _context.Book.FindAsync(Id);
                if (Book == null)
                {
                    return null;
                }
                _context.Book.Remove(Book);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return await (Task<bool>)Task.FromException(ex);
            }
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetAllAsync()
        {
            try
            {
                return await _context.Book.Include(i => i.Editorial).Include(i => i.Author).ToListAsync();
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<IEnumerable<Book>>>)Task.FromException(ex);
            }
        }


        public async Task<ActionResult<Book?>> GetByIdAsync(int id)
        {
            try
            {
                var Book = await _context.Book.Include(i => i.Editorial).Include(i => i.Author).FirstOrDefaultAsync(i => i.Id == id);
                return Book;
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<Book?>>)Task.FromException(ex);
            }
        }

        public async Task<Book?> UpdateAsync(Book entity)
        {
            try
            {
                var Book = _context.Book.Find(entity.Id);
                if (Book == null)
                {
                    return null;
                }
                Book.Title = entity.Title;
                Book.Year = entity.Year;
                Book.PageCount = entity.PageCount;
                Book.EditorialId = entity.EditorialId;
                Book.AuthorId = entity.AuthorId;
                _context.Entry(Book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Book;
            }
            catch (Exception ex)
            {
                return await (Task<Book>)Task.FromException(ex);
            }
        }
        public async Task<int> CountByEditorial(int editorialId)
        {
            try
            {
                var query = _context.Book.Where(p => p.EditorialId == editorialId).Count();
                return query;

            }
            catch (Exception ex)
            {
                return await (Task<int>)Task.FromException(ex);
            }
        }
    }
}
