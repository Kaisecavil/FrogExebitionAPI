using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrogExebitionAPI.Database;
using FrogExebitionAPI.Models;
using Microsoft.IdentityModel.Tokens;
using FrogExebitionAPI.UoW;
using FrogExebitionAPI.Services;
using FrogExebitionAPI.Exceptions;

namespace FrogExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrogsController : ControllerBase
    {
        private readonly ILogger<FrogsController> _logger;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IFrogService _frogService;

        public FrogsController(ILogger<FrogsController> logger/*, IUnitOfWork unitOfWork*/, IFrogService frogService)
        {
            _logger = logger;
            //_unitOfWork = unitOfWork;
            _frogService = frogService;
        }

        // GET: api/Frogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Frog>>> GetFrogs()
        {
            try
            {
                return base.Ok(await _frogService.GetAllFrogs());
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }
            //return await _unitOfWork.Frogs.GetAllAsync(true);

        }

        // GET: api/Frogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Frog>> GetFrog(Guid id)
        {
            try
            {
                return base.Ok(await _frogService.GetFrog(id));
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }

            //if (await _unitOfWork.Frogs.IsEmpty())
            //{
            //    return base.NotFound();
            //}
            //var frog = await _unitOfWork.Frogs.GetAsync(id,true);

            //if (frog == null)
            //{
            //    return base.NotFound();
            //}

            //return base.Ok(frog);
        }

        // PUT: api/Frogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrog(Guid id, Frog frog)
        {
            try
            {
                //ModelState.IsValid
                await _frogService.UpdateFrog(id, frog);
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

            //if (id != frog.Id)
            //{
            //    return base.BadRequest();
            //}

            ////_context.Entry(frog).State = EntityState.Modified; ?????

            //try
            //{
            //    if (!await _unitOfWork.Frogs.EntityExists(id))
            //    {
            //        return base.NotFound();
            //    }
            //    await _unitOfWork.Frogs.UpdateAsync(frog);

            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!await _unitOfWork.Frogs.EntityExists(id))
            //    {
            //        return base.NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return base.NoContent();

        }

        // POST: api/Frogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Frog>> PostFrog(Frog frog)
        {
            try
            {
                var createdFrog = await _frogService.CreateFrog(frog);
                return base.CreatedAtAction("GetFrog", new { id = createdFrog.Id }, createdFrog);
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return base.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return base.NotFound(ex.Message);
            }
            ////_unitOfWork.Frogs.GetAll().IsNullOrEmpty() 
            //if (await _unitOfWork.Frogs.IsEmpty())
            //{
            //    return base.Problem("Entity set 'ApplicationContext.Frogs'  is null.");
            //}
            //await _unitOfWork.Frogs.CreateAsync(frog);

            //return base.CreatedAtAction("GetFrog", new { id = frog.Id }, frog);

        }

        // DELETE: api/Frogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrog(Guid id)
        {
            try
            {
                await _frogService.DeleteFrog(id);
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
            catch (Exception ex)
            {
                return base.NotFound(ex.Message);
            }
            //if (await _unitOfWork.Frogs.IsEmpty())
            //{
            //    return base.NotFound();
            //}
            //var frog = await _unitOfWork.Frogs.GetAsync(id);

            //if (frog == null)
            //{
            //    return base.NotFound();
            //}

            //await _unitOfWork.Frogs.DeleteAsync(frog.Id);

            //return base.NoContent();

        }

        //private bool FrogExists(Guid id)
        //{
        //    return _unitOfWork.Frogs.GetAsync(id,true).Result != null;
        //}
    }
}
