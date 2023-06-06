using Microsoft.AspNetCore.Mvc;
using FrogExebitionAPI.Database;
using FrogExebitionAPI.Models;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ExebitionExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExebitionsController : ControllerBase
    {
        private readonly ILogger<ExebitionsController> _logger;
        private readonly IExebitionService _exebitionService;

        public ExebitionsController(ILogger<ExebitionsController> logger, IExebitionService exebitionService)
        {
            _logger = logger;
            _exebitionService = exebitionService;
        }

        // GET: api/Exebitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exebition>>> GetExebitions()
        {
            try
            {
                return base.Ok(await _exebitionService.GetAllExebitions());
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }

        }

        // GET: api/Exebitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exebition>> GetExebition(Guid id)
        {
            try
            {
                return base.Ok(await _exebitionService.GetExebition(id));
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }


        }

        // PUT: api/Exebitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExebition(Guid id, Exebition exebition)
        {
            try
            {
                //ModelState.IsValid
                await _exebitionService.UpdateExebition(id, exebition);
                return base.NoContent();
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return base.BadRequest(ex.Message);
            }
        }

        // POST: api/Exebitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exebition>> PostExebition(Exebition exebition)
        {
            try
            {
                var createdExebition = await _exebitionService.CreateExebition(exebition);
                return base.CreatedAtAction("GetExebition", new { id = createdExebition.Id }, createdExebition);
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return base.BadRequest(ex.Message);
            }
            //catch (DbUpdateException ex)
            //{
            //    return base.Ba
            //}
        }

        // DELETE: api/Exebitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExebition(Guid id)
        {
            try
            {
                await _exebitionService.DeleteExebition(id);
                return base.NoContent();
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }
        }
    }
}
