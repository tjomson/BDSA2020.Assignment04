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
    public class TagRepositoryTests
    {
        private readonly SqliteConnection _connection;
        private readonly KanbanContext _context;
        private readonly TagRepository _repository;
        public TagRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>().UseSqlite(_connection);
            _context = new KanbanContext(builder.Options);
            _context.Database.EnsureCreated();
            _context.GenerateTestData();

            _repository = new TagRepository(_context);

        }

        [Fact]
        public void Create_given_tag()
        {
            var tag = new TagCreateDTO()
            {
                Name = "Bob"
            };
            var actual = _repository.Create(tag);

            var entity = _context.Tags.Find(actual.tagId);

            Assert.Equal("Bob",entity.Name);


        }

        [Theory]
        [InlineData(1,Response.Deleted, false)]
        public void DeleteTest(int tagId,Response expected, bool force)
        {
            var actual = _repository.Delete(tagId, force);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateTest()
        {
            var tagupdate = new TagUpdateDTO()
            {
                Id = 1,
                Name = "BobDenAnden"
            };

            var actualResponse = _repository.Update(tagupdate);

            var actualName = _context.Tags.Find(1).Name;
            Assert.Equal("BobDenAnden",actualName);
            Assert.Equal(Response.Updated,actualResponse);
        }

        public void Dispose()
        {
            _connection.Dispose();
            _context.Dispose();
        }

    }
}
