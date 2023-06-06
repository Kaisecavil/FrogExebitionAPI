using FrogExebitionAPI.Dto;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrogExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrogOnExebitionsController : ControllerBase
    {
        private readonly ILogger<FrogOnExebitionsController> _logger;
        private readonly IFrogOnExebitionService _frogOnExebitionService;

        public FrogOnExebitionsController(ILogger<FrogOnExebitionsController> logger, IFrogOnExebitionService frogOnExebitionService)
        {
            _logger = logger;
            _frogOnExebitionService = frogOnExebitionService;
        }

        // GET: api/FrogOnExebitions
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FrogOnExebition>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<FrogOnExebition>>> GetFrogOnExebitions()
        {
            try
            {
                return base.Ok(await _frogOnExebitionService.GetAllFrogOnExebitions());
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }

        }

        // GET: api/FrogOnExebitions/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(FrogOnExebition))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FrogOnExebition>> GetFrogOnExebition(Guid id)
        {
            try
            {
                return base.Ok(await _frogOnExebitionService.GetFrogOnExebition(id));
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }


        }

        // PUT: api/FrogOnExebitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutFrogOnExebition(Guid id, FrogOnExebition frogOnExebition)
        {
            try
            {
                //ModelState.IsValid
                //ModelState.AddModelError("")
                await _frogOnExebitionService.UpdateFrogOnExebition(id, frogOnExebition);
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

        // POST: api/FrogOnExebitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(FrogOnExebition))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FrogOnExebition>> PostFrogOnExebition(FrogOnExebition frogOnExebition)
        {
            try
            {
                var createdFrogOnExebition = await _frogOnExebitionService.CreateFrogOnExebition(frogOnExebition);
                return base.CreatedAtAction("GetFrogOnExebition", new { id = createdFrogOnExebition.Id }, createdFrogOnExebition);
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

        // DELETE: api/FrogOnExebitions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteFrogOnExebition(Guid id)
        {
            try
            {
                await _frogOnExebitionService.DeleteFrogOnExebition(id);
                return base.NoContent();
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }
        }
    }

}
