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

namespace ExebitionExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExebitionsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ExebitionsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ExebitionsController(ApplicationContext context, ILogger<ExebitionsController> logger, IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Exebitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exebition>>> GetExebitions()
        {
            //_logger.LogInformation("Processing request!!!!!!!!!!!!!!!!");
            return await _unitOfWork.Exebitions.GetAllAsync();
        }

        // GET: api/Exebitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exebition>> GetExebition(Guid id)
        {

            if (_unitOfWork.Exebitions.GetAll().IsNullOrEmpty())
            {
                return base.NotFound();
            }
            var exebition = await _unitOfWork.Exebitions.GetAsync(id);

            if (exebition == null)
            {
                return base.NotFound();
            }

            return exebition;
        }

        // PUT: api/Exebitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExebition(Guid id, Exebition exebition)
        {

            if (id != exebition.Id)
            {
                return base.BadRequest();
            }

            //_context.Entry(exebition).State = EntityState.Modified; ?????

            try
            {
                if (!ExebitionExists(id))
                {
                    return base.NotFound();
                }
                await _unitOfWork.Exebitions.UpdateAsync(exebition);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExebitionExists(id))
                {
                    return base.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return base.NoContent();

        }

        // POST: api/Exebitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exebition>> PostExebition(Exebition exebition)
        {

            if (_unitOfWork.Exebitions.GetAll().IsNullOrEmpty())
            {
                return base.Problem("Entity set 'ApplicationContext.Exebitions'  is null.");
            }
            await _unitOfWork.Exebitions.CreateAsync(exebition);

            return base.CreatedAtAction("GetExebition", new { id = exebition.Id }, exebition);

        }

        // DELETE: api/Exebitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExebition(Guid id)
        {

            if (_unitOfWork.Exebitions.GetAll().IsNullOrEmpty())
            {
                return base.NotFound();
            }
            var exebition = await _unitOfWork.Exebitions.GetAsync(id);
            if (exebition == null)
            {
                return base.NotFound();
            }

            await _unitOfWork.Exebitions.DeleteAsync(exebition.Id);

            return base.NoContent();

        }

        private bool ExebitionExists(Guid id)
        {
            return _unitOfWork.Exebitions.GetAsync(id).Result != null;
        }
    }
}
