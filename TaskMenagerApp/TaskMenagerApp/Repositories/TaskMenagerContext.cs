using Microsoft.EntityFrameworkCore;
using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public class TaskMenagerContext : DbContext
    {
        public TaskMenagerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MyTask> Tasks { get; set;} // DBset dla encji ktora jest mapowana na tabele w DB

    }
}
