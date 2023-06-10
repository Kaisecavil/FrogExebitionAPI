using FrogExebitionAPI.DTO.ApplicatonUserDTOs;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrogExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IApplicationUserService _userService;

        public UsersController(ILogger<UsersController> logger, IApplicationUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ApplicationUserDtoGeneral>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ApplicationUserDtoGeneral>>> GetUsers()
        {
            try
            {
                return base.Ok(await _userService.GetAllApplicationUsers());
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }

        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApplicationUserDtoDetail))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApplicationUserDtoDetail>> GetUser(Guid id)
        {
            try
            {
                return base.Ok(await _userService.GetApplicationUser(id));
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }


        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> PutUser(Guid id, ApplicationUserDtoForUpdate user)
        {
            try
            {
                //ModelState.IsValid
                //ModelState.AddModelError("")
                await _userService.UpdateApplicationUser(id, user);
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

        //// POST: api/Users
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[ProducesResponseType(201, Type = typeof(ApplicationUserDtoDetail))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(422)]
        //public async Task<ActionResult<ApplicationUserDtoDetail>> PostUser(ApplicationUserDtoForCreate user)
        //{
        //    try
        //    {
        //        var createdApplicationUser = await _userService.CreateApplicationUser(user);
        //        return base.CreatedAtAction("GetUser", new { id = createdApplicationUser.Id }, createdApplicationUser);
        //    }
        //    catch (BadRequestException ex)
        //    {
        //        return base.BadRequest(ex.Message);
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return base.UnprocessableEntity(ex.Message);
        //    }
        //}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteApplicationUser(id);
                return base.NoContent();
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }
        }
    }
}
