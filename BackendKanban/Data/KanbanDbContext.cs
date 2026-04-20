using Microsoft.EntityFrameworkCore;

namespace BackendKanban.Data
{
    public class KanbanDbContext : DbContext
    {

        public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Tarefa> Tarefa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Host=localhost;Port=5433;Database=kanban_db;Username=postgres;Password=tudocerto240405g";
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}