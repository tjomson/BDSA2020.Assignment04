using System;
using Xunit;
using BDSA2020.Assignment04.Entities;
using BDSA2020.Assignment04.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        

        
        
        public void Dispose()
        {
            _connection.Dispose();
            _context.Dispose();
        }
    }
}
