using Microsoft.EntityFrameworkCore;
using TECHTALKFORUM.Data;
using TECHTALKFORUM.Dtos;
using TECHTALKFORUM.DTOs;
using TECHTALKFORUM.Models;

namespace TECHTALKFORUM.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MessageDto>> GetAllAsync()
        {
            return await _context.Messages
                .Include(m => m.User)
                .Include(m => m.Channel)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    CreatedAt = m.CreatedAt,
                    Username = m.User.Username,
                    ChannelName = m.Channel.Name
                })
                .ToListAsync();
        }

        public async Task<MessageDto?> GetByIdAsync(int id)
        {
            return await _context.Messages
                .Include(m => m.User)
                .Include(m => m.Channel)
                .Where(m => m.Id == id)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    CreatedAt = m.CreatedAt,
                    Username = m.User.Username,
                    ChannelName = m.Channel.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MessageDto> CreateAsync(MessageCreateDto dto)
        {
            var message = new Message
            {
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId,
                ChannelId = dto.ChannelId
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                Username = _context.Users.Find(message.UserId)?.Username ?? "Unknown",
                ChannelName = _context.Channels.Find(message.ChannelId)?.Name ?? "Unknown"
            };
        }

        public async Task<bool> UpdateAsync(int id, MessageCreateDto dto)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) return false;

            message.Content = dto.Content;
            message.ChannelId = dto.ChannelId;
            message.UserId = dto.UserId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

