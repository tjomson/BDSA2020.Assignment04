using System;
using Xunit;
using BDSA2020.Assignment04.Entities;
using BDSA2020.Assignment04.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BDSA2020.Assignment04.Models.Tests
{
    public class TaskRepositoryTests : IDisposable
    {

        private readonly SqliteConnection _connection;
        private readonly KanbanContext _context;
        private readonly TaskRepository _repository;

        public TaskRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>().UseSqlite(_connection);
            _context = new KanbanContext(builder.Options);
            _context.Database.EnsureCreated();
            _context.GenerateTestData();

            _repository = new TaskRepository(_context);
            
        }
        [Fact]
        public void Create_task_given_input()
        {
            var input = new TaskCreateDTO{
                Title = "test",
                Description = "Beskrivelse",
                AssignedToId = 2,
                Tags = new List<string>(){"a tag", "another tag"}
            };

            var actual = _repository.Create(input);

            Assert.Equal((Response.Created, 6),actual);
        }

        [Theory]
        [InlineData (Response.Deleted,1)]
        [InlineData (Response.Deleted,2)]
        [InlineData (Response.Conflict,3)]
        [InlineData (Response.Conflict,4)]
        [InlineData (Response.Conflict,5)]
        public void Delete_Task_given_taskids(Response expected,int i)
        {
            var actual = _repository.Delete(i);

            Assert.Equal(expected,actual);
        }

        [Fact]
        public void ReadAll_should_return_all()
        {
            var expected = new int []{1,2,3,4,5};
            var counter = 0;
            
            foreach (var TaskDTO in _repository.Read(true))
            {
                 Assert.Equal(expected[counter], TaskDTO.Id);
                counter++;
            }
            

        }
        
        [Fact]
        public void ReadAll_should_not_Return_Removed()
        {
            var actual = _repository.Read(false);
            var expectedIds = new int[]{1,2,4,5};
            var counter = 0;
            foreach (var task in actual)
            {
                Assert.Equal(expectedIds[counter], task.Id);
                counter++;
            }
        }

        
        [Fact]
        public void Read_Single_Task()
        {
            var actual = _repository.Read(1);
            
            Assert.Equal("kekw", actual.Title);
            Assert.Equal(State.New, actual.State);
        }
        
        [Fact]
        public void Update_test()
        {
            var entity = new TaskUpdateDTO{
                State = State.New
            };
                       
            var actuel = _repository.Update(entity);

            Assert.Equal(Response.Updated,actuel);


        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}
