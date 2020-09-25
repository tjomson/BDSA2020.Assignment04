using BDSA2020.Assignment04.Models;
using BDSA2020.Assignment04.Entities;
using System.Collections.Generic;

namespace BDSA2020.Assignment04.Models.Tests{
    public static class TestDataGenerator
    {
        public static void GenerateTestData(this KanbanContext context)
        {
            var user = new User
            {
                Name = "Clark Kent",
                EmailAddress = "Superman@PlanetMetropolitan.com",
                Tasks = new List<Task>()
                {
                    new Task { Title = "kekw", State = State.New } ,
                    new Task {Title = "pepehands", State = State.Active },
                    new Task { Title = "pogchamp", State = State.Removed } 
                }
               
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
            context.SaveChanges();
        }
    }

}