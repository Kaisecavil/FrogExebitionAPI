using Microsoft.AspNetCore.Mvc;
using FrogExebitionAPI.Database;
using FrogExebitionAPI.Models;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using FrogExebitionAPI.DTO.ExebitionDTOs;
using FrogExebitionAPI.DTO.FrogDTOs;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExebitionDtoDetail>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ExebitionDtoDetail>>> GetExebitions()
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
        [ProducesResponseType(200, Type = typeof(ExebitionDtoDetail))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ExebitionDtoDetail>> GetExebition(Guid id)
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
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutExebition(Guid id, ExebitionDtoForCreate exebition)
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
        [ProducesResponseType(201, Type = typeof(IEnumerable<ExebitionDtoDetail>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ExebitionDtoDetail>> PostExebition(ExebitionDtoForCreate exebition)
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
        }

        // DELETE: api/Exebitions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
