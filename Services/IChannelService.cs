using TECHTALKFORUM.DTOs;

namespace TECHTALKFORUM.Services;

public interface IChannelService
{
    Task<IEnumerable<ChannelDto>> GetAllAsync();
    Task<ChannelDto?> GetChannelByIdAsync(int id);
    Task<ChannelDto> CreateChannelAsync(ChannelCreateDto dto);
    Task<bool> UpdateChannelAsync(int id, ChannelCreateDto dto);
    Task<bool> DeleteChannelAsync(int id);
}


