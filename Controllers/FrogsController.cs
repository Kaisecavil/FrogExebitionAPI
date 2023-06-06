﻿using System;
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
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.DTO.FrogDTOs;

namespace FrogExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrogsController : ControllerBase
    {
        private readonly ILogger<FrogsController> _logger;
        private readonly IFrogService _frogService;

        public FrogsController(ILogger<FrogsController> logger, IFrogService frogService)
        {
            _logger = logger;
            _frogService = frogService;
        }

        // GET: api/Frogs
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FrogDto>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<FrogDto>>> GetFrogs()
        {
            try
            {
                return base.Ok(await _frogService.GetAllFrogs());
            }
            catch (NotFoundException ex)
            {
                return base.NotFound(ex.Message);
            }

        }

        // GET: api/Frogs/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Frog))]
        [ProducesResponseType(404)]
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

           
        }

        // PUT: api/Frogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutFrog(Guid id, Frog frog)
        {
            try
            {
                //ModelState.IsValid
                //ModelState.AddModelError("")
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
        }

        // POST: api/Frogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Frog))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        }

        // DELETE: api/Frogs/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
        }
    }
}