using TECHTALKFORUM.Dtos;
using TECHTALKFORUM.DTOs;

namespace TECHTALKFORUM.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetAllAsync();
        Task<MessageDto?> GetByIdAsync(int id);
        Task<MessageDto> CreateAsync(MessageCreateDto dto);
        Task<bool> UpdateAsync(int id, MessageCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

