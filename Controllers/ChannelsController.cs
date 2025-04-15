using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TECHTALKFORUM.DTOs;
using TECHTALKFORUM.Services;

namespace TECHTALKFORUM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelsController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public ChannelsController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        // GET: api/channels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChannelDto>>> Get()
        {
            var channels = await _channelService.GetAllAsync();
            return Ok(channels);
        }

        // GET: api/channels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChannelDto>> GetChannel(int id)
        {
            var channel = await _channelService.GetChannelByIdAsync(id);
            if (channel == null)
                return NotFound();

            return Ok(channel);
        }

        // POST: api/channels
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ChannelDto>> CreateChannel(ChannelCreateDto dto)
        {
            var created = await _channelService.CreateChannelAsync(dto);
            return CreatedAtAction(nameof(GetChannel), new { id = created.Id }, created);
        }

        // PUT: api/channels/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateChannel(int id, ChannelCreateDto dto)
        {
            var success = await _channelService.UpdateChannelAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/channels/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            var success = await _channelService.DeleteChannelAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

