using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Modules.Speakers.Core.DTO;
using ModularMonolith.Modules.Speakers.Core.Services;

namespace ModularMonolith.Modules.Speakers.Api.Controllers
{
    internal class SpeakersController : BaseController
    {
        private readonly ISpeakersService _speakersService;

        public SpeakersController(ISpeakersService service)
        {
            _speakersService = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SpeakerDto>> Get(Guid id)
        {
            var speaker = await _speakersService.GetAsync(id);
            if (speaker is null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> Get() => Ok(await _speakersService.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> Post(SpeakerDto speaker)
        {
            await _speakersService.CreateAsync(speaker);
            return CreatedAtAction(nameof(Get), new {id = speaker.Id}, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(SpeakerDto speaker)
        {
            await _speakersService.UpdateAsync(speaker);
            return NoContent();
        }
    }
}