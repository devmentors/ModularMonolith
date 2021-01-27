using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Modules.Conferences.Core.DTO;
using ModularMonolith.Modules.Conferences.Core.Services;

namespace ModularMonolith.Modules.Conferences.Api.Controllers
{
    internal class HostsController : BaseController
    {
        private readonly IHostService _hostService;

        public HostsController(IHostService hostService)
        {
            _hostService = hostService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HostDetailsDto>> Get(Guid id)
        {
            var host = await _hostService.GetAsync(id);
            if (host is null)
            {
                return NotFound();
            }

            return Ok(host);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HostDto>>> BrowseAsync()
            => Ok(await _hostService.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync(HostDetailsDto dto)
        {
            await _hostService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new {id = dto.Id}, null);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, HostDetailsDto dto)
        {
            dto.Id = id;
            await _hostService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _hostService.DeleteAsync(id);
            return NoContent();
        }
    }
}