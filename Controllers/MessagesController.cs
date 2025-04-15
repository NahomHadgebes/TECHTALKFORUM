using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TECHTALKFORUM.Dtos;
using TECHTALKFORUM.DTOs;
using TECHTALKFORUM.Services;

namespace TECHTALKFORUM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> Get()
        {
            var messages = await _messageService.GetAllAsync();
            return Ok(messages);
        }

        // GET: api/messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> GetById(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            return Ok(message);
        }

        // POST: api/messages
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MessageDto>> Create(MessageCreateDto dto)
        {
            var createdMessage = await _messageService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdMessage.Id }, createdMessage);
        }

        // PUT: api/messages/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, MessageCreateDto dto)
        {
            var result = await _messageService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/messages/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _messageService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}


