using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BDSA2020.Assignment04.Entities
{
    public class KanbanContext : DbContext
    {
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TaskTag> TaskTags { get; set; }

        public KanbanContext() { }

        public KanbanContext(DbContextOptions<KanbanContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TaskTag>().HasKey(taskTag => new { taskTag.TagId, taskTag.TaskId });


            modelBuilder.Entity<User>()
                        .HasIndex(t => t.EmailAddress)
                        .IsUnique();

            modelBuilder.Entity<Tag>()
                        .HasIndex(t => t.Name)
                        .IsUnique();



            var addtag = new Tag { Id = 1, Name = "BobsTag" };
            new Tag { Id = 1, Name = "brorøjeblik" };
            new Tag { Id = 2, Name = "UwU" };
            var taskTags1 = new List<TaskTag>();
            taskTags1.Add(new TaskTag { TaskId = 1, TagId = 1 });
            taskTags1.Add(new TaskTag { TaskId = 1, TagId = 2 });



            var tags2 = new List<Tag>();
            tags2.Add(new Tag { Id = 3, Name = "ehhh" });
            tags2.Add(new Tag { Id = 4, Name = "-_-" });
            var taskTags2 = new List<TaskTag>();
            taskTags2.Add(new TaskTag { TaskId = 2, TagId = 3 });
            taskTags2.Add(new TaskTag { TaskId = 2, TagId = 4 });



            var tasks1 = new List<Task>()
                {
                    new Task { Id =  1, Title = "kekw", AssignedToId = 2, State = State.New, /*Tags = new List<TaskTag>(){ new TaskTag{TaskId = 1, TagId = 1}, new TaskTag{TaskId = 1, TagId = 2}}*/ } ,
                    new Task { Id =  2, Title = "pepehands", AssignedToId = 2, State = State.Active },
                    new Task { Id =  3, Title = "pogchamp", AssignedToId = 1, State = State.Removed }
                };

            var user = new User
            {
                Id = 1,
                Name = "Clark Kent",
                EmailAddress = "Superman@PlanetMetropolitan.com"

            };

            var user2 = new User
            {
                Id = 2,
                Name = "Bob",
                EmailAddress = "bob@email.com"

            };



            var users = new []{user,user2};
            var tags = new []{addtag};

            modelBuilder.Entity<Task>().HasData(tasks1);
            modelBuilder.Entity<Tag>().HasData(tags);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<TaskTag>().HasData(taskTags1);

        }
    }
}