using Microsoft.EntityFrameworkCore;

namespace BDSA2020.Assignment04.Entities
{
    public class KanbanContext : DbContext
    {
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TaskTag> TaskTags {get; set; }

        public KanbanContext() { }

        public KanbanContext(DbContextOptions<KanbanContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasIndex(t => t.EmailAddress)
                        .IsUnique();

            modelBuilder.Entity<Tag>()
                        .HasIndex(t => t.Name)
                        .IsUnique();


            modelBuilder.Entity<TaskTag>().HasKey(taskTag => new {taskTag.TagId,taskTag.TaskId});
        }
    }
}