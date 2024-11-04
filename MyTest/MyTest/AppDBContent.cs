using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyTest.Models;

namespace MyTest
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
    }
}
