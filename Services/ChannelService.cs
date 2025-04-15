using Microsoft.EntityFrameworkCore;
using TECHTALKFORUM.Data;
using TECHTALKFORUM.DTOs;
using TECHTALKFORUM.Models;
using TECHTALKFORUM.Services;


public class ChannelService : IChannelService
{
    private readonly AppDbContext _context;

    public ChannelService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ChannelDto>> GetAllAsync()
    {
        return await _context.Channels
            .Select(c => new ChannelDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToListAsync();
    }

    public async Task<ChannelDto?> GetChannelByIdAsync(int id)
    {
        return await _context.Channels
            .Where(c => c.Id == id)
            .Select(c => new ChannelDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ChannelDto> CreateChannelAsync(ChannelCreateDto dto)
    {
        var channel = new Channel
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _context.Channels.Add(channel);
        await _context.SaveChangesAsync();

        return new ChannelDto
        {
            Id = channel.Id,
            Name = channel.Name,
            Description = channel.Description
        };
    }

    public async Task<bool> UpdateChannelAsync(int id, ChannelCreateDto dto)
    {
        var channel = await _context.Channels.FindAsync(id);
        if (channel == null)
            return false;

        channel.Name = dto.Name;
        channel.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteChannelAsync(int id)
    {
        var channel = await _context.Channels.FindAsync(id);
        if (channel == null)
            return false;

        _context.Channels.Remove(channel);
        await _context.SaveChangesAsync();
        return true;
    }
}


