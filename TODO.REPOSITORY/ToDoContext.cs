using Microsoft.EntityFrameworkCore;
using TODO.MODELS.DataModels;

namespace TODO.REPOSITORY
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> opt) : base(opt)
        {

        }

        public DbSet<ToDoDataModel> ToDo { get; set; }

        // Adding fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoDataModel>().Property(p => p.Status).HasDefaultValue(false);
            modelBuilder.Entity<ToDoDataModel>().Property(p => p.CreatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ToDoDataModel>().Property(p => p.CreatedBy).HasDefaultValue("");
            modelBuilder.Entity<ToDoDataModel>().Property(p => p.ModifiedBy).HasDefaultValue("");
            modelBuilder.Entity<ToDoDataModel>().Property(p => p.ModifiedOn).HasDefaultValueSql("getdate()");
        }
    }
}
