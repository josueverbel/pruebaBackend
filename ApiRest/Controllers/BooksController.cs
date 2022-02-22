#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest.Data;
using ApiRest.Models;
using ApiRest.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ApiRest.DTOS.Requests;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly BookService _service;
        private readonly EditorialService _editorialService;
        private readonly AuthorService _authorService;
        public BooksController(BookService service, AuthorService authorService, EditorialService editorialService)
        {
            _service = service;
            _editorialService = editorialService;
            _authorService = authorService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<IEnumerable<Book>>>)Task.FromException(ex);
            }


        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return await (Task<Book>)Task.FromException(ex);
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateRequest request)
        {
            if (id != request.Id)
            {
                ModelStateDictionary bad = new ModelStateDictionary();
                bad.AddModelError("Id", "The comparison of the id failed");
                return BadRequest(bad);
            }

            try
            {
                var entity = await _service.UpdateAsync(request.ToBook());
                return Ok(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookRequest request)
        {
            
            
            try
            {
                var author = await _authorService.GetByIdAsync(request.AuthorId);

                ModelStateDictionary bad = new ModelStateDictionary();
                if (author == null || author.Value == null)
                {
                    bad.AddModelError("Author Id", "The author is not registered");
                    return BadRequest(bad);
                }
                var editorial = await _editorialService.GetByIdAsync(request.EditorialId);
                
                
                if (editorial == null || editorial.Value == null)
                {
                    bad.AddModelError("Editorial", "The Editorial is not registered");
                    return BadRequest(bad);
                }
                if ((editorial.Value.MaxCount != -1) && ( await _service.CountByEditorial(request.EditorialId) >= (editorial.Value.MaxCount - 1))){
                   
                    bad.AddModelError("Editorial", "The maximum number of books allowed for the Editorial has been completed");
                    return BadRequest(bad);
                }
                Book Book = await _service.CreateAsync(request.ToBook());
                
                return CreatedAtAction("GetBook", new { id = Book.Id }, Book);
            }
            catch (Exception ex)
            {
                return await (Task<Book>)Task.FromException(ex);
            }

        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (result.HasValue && result.Value)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return await (Task<IActionResult>)Task.FromException(ex);
            }


        }
    }
}
