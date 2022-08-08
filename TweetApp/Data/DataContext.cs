using Microsoft.EntityFrameworkCore;
using TweetApp.Models;

namespace TweetApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
    }
}
