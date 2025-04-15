using Microsoft.EntityFrameworkCore;
using TECHTALKFORUM.Models;

namespace TECHTALKFORUM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Channel> Channels { get; set; }

        public DbSet<Message> Messages { get; set; }


    }
}

