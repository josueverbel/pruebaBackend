#nullable disable
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest.Models;
using ApiRest.DTOS.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ApiRest.Services;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialsController : ControllerBase
    {
        private readonly EditorialService _service;
        public EditorialsController(EditorialService service)
        {
            _service = service;
        }

        // GET: api/Editorials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetEditorial()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                return await (Task<ActionResult< IEnumerable<Editorial>>>)Task.FromException(ex);
            }


        }

        // GET: api/Editorials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Editorial>> GetEditorial(int id)
        {
            try
            {
              return await  _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return await (Task<Editorial>)Task.FromException(ex);
            }
        }

        // PUT: api/Editorials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEditorial(int id, EditorialUpdateRequest request)
        {
            if (id != request.Id)
            {
                ModelStateDictionary bad = new ModelStateDictionary();
                bad.AddModelError("Id", "The comparison of the id failed");
                return BadRequest(bad);
            }

            try
            {
                var entity = await _service.UpdateAsync(request.ToEditorial());
                return Ok(entity);
            }
            catch (DbUpdateConcurrencyException ex)
            {    throw;
                
            }

        }

        // POST: api/Editorials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Editorial>> PostEditorial(EditorialRequest request)
        {
            try
            {
                Editorial editorial = await _service.CreateAsync(request.ToEditorial());
                return CreatedAtAction("GetEditorial", new { id = editorial.Id }, editorial);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return await (Task<Editorial>)Task.FromException(ex);
            }

        }

        // DELETE: api/Editorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditorial(int id)
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
