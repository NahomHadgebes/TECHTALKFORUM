using System.ComponentModel.DataAnnotations;

namespace TECHTALKFORUM.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public int UserId { get; set; }
        public int ChannelId { get; set; }

        // Navigation Properties
        public User? User { get; set; }
        public Channel? Channel { get; set; }
    }
}

