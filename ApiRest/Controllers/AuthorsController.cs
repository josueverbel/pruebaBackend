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
using ApiRest.DTOS.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Cors;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;
        public AuthorsController(AuthorService service)
        {
            _service = service;
        }

        // GET: api/Authors
        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult<IEnumerable<Author>>>)Task.FromException(ex);
            }


        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return await (Task<Author>)Task.FromException(ex);
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateRequest request)
        {
            if (id != request.Id)
            {
                ModelStateDictionary bad = new ModelStateDictionary();
                bad.AddModelError("Id", "The comparison of the id failed");
                return BadRequest(bad);
            }

            try
            {
                var entity = await _service.UpdateAsync(request.ToAuthor());
                return Ok(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorRequest request)
        {
            try
            {
                Author Author = await _service.CreateAsync(request.ToAuthor());
                return CreatedAtAction("GetAuthor", new { id = Author.Id }, Author);
            }
            catch (Exception ex)
            {
                return await (Task<Author>)Task.FromException(ex);
            }

        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
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
