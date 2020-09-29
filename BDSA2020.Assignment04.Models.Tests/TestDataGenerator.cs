using BDSA2020.Assignment04.Models;
using BDSA2020.Assignment04.Entities;
using System.Collections.Generic;

namespace BDSA2020.Assignment04.Models.Tests{
    public static class TestDataGenerator
    {
        public static void GenerateTestData(this KanbanContext context)
        {
            var addtag = new Tag{Id = 1, Name = "BobsTag"};
            new Tag{Id = 1, Name = "brorøjeblik"};
            new Tag{Id = 2, Name = "UwU"};
            var taskTags1 = new List<TaskTag>();
            taskTags1.Add(new TaskTag{TaskId = 1, TagId = 1});
            taskTags1.Add(new TaskTag{TaskId = 1, TagId = 2});

            

            var tags2 = new List<Tag>();
            tags2.Add(new Tag{Id = 3, Name = "ehhh"});
            tags2.Add(new Tag{Id = 4, Name = "-_-"});
            var taskTags2 = new List<TaskTag>();
            taskTags2.Add(new TaskTag{TaskId = 2, TagId = 3});
            taskTags2.Add(new TaskTag{TaskId = 2, TagId = 4});



            var tasks1 = new List<Task>()
                {
                    new Task { Title = "kekw", State = State.New, /*Tags = new List<TaskTag>(){ new TaskTag{TaskId = 1, TagId = 1}, new TaskTag{TaskId = 1, TagId = 2}}*/ } ,
                    new Task { Title = "pepehands", State = State.Active },
                    new Task { Title = "pogchamp", State = State.Removed } 
                };

            var user = new User
            {
                Name = "Clark Kent",
                EmailAddress = "Superman@PlanetMetropolitan.com",
                Tasks = tasks1
               
            };

            var user2 = new User
            {
                Name = "Bob",
                EmailAddress = "bob@email.com",
                Tasks = new List<Task>()
                {
                    new Task {Title = "do something", State = State.Closed},
                    new Task {Title = "bruh", State = State.Resolved}
                }
                
            };

            

            context.Users.AddRange(user, user2);
            context.Tags.Add(addtag);
            context.SaveChanges();
        }
    }

}