using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TECHTALKFORUM.Models
{
    public class Channel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();


    }
}

