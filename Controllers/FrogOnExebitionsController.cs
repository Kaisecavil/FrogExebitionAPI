using FrogExebitionAPI.DTO.FrogOnExebitionDTOs;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<FrogOnExebitionDtoDetail>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<FrogOnExebitionDtoDetail>>> GetFrogOnExebitions()
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
        [ProducesResponseType(200, Type = typeof(FrogOnExebitionDtoDetail))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<FrogOnExebitionDtoDetail>> GetFrogOnExebition(Guid id)
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
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutFrogOnExebition(Guid id, FrogOnExebitionDtoForCreate frogOnExebition)
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
            catch (DbUpdateException ex)
            {
                return base.UnprocessableEntity(ex.Message);
            }
        }

        // POST: api/FrogOnExebitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(FrogOnExebitionDtoDetail))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FrogOnExebitionDtoDetail>> PostFrogOnExebition(FrogOnExebitionDtoForCreate frogOnExebition)
        {
            try
            {
                var createdFrogOnExebition = await _frogOnExebitionService.CreateFrogOnExebition(frogOnExebition);
                return base.CreatedAtAction("GetFrogOnExebition", new { id = createdFrogOnExebition.Id }, createdFrogOnExebition);
            }
            catch (BadRequestException ex)
            {
                return base.BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return base.UnprocessableEntity(ex.Message);
            }
        }

        // DELETE: api/FrogOnExebitions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
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
