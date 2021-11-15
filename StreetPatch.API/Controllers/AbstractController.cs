using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using StreetPatch.Data.Entities.Base;
using StreetPatch.Data.Repositories.Interfaces;

namespace StreetPatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AbstractController<TEntity, TRepository> : ControllerBase
        where TEntity : EntityBase
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;

        protected AbstractController(TRepository repository)
        {
            this.repository = repository;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAllAsync();
        }

        // GET: api/[controller]/5
        [HttpGet("{id:guid}", Name = "Get")]
        public async Task<ActionResult<TEntity>> Get(Guid id)
        {
            var entity = await repository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        // PUT: api/[controller]/{id:guid}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(entity);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            await repository.AddAsync(entity);
            return CreatedAtRoute("api/[controller]/{id:guid}", new { id = entity.Id }, entity);
        }

        // DELETE: api/[controller]/{id:guid}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<TEntity>> Delete(Guid id)
        {
            var entity = await repository.DeleteAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }
    }
}
