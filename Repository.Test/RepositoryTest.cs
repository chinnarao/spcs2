using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Extensions;
using Xunit;

namespace West.EntityFrameworkCore.Repository.Tests
{
    public class RepositoryTestContextTest
    {
        public IDbConnection Connection { get; set; }

        private IServiceProvider GetServiceProvider()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();

            var services = new ServiceCollection()
                .AddDbContext<TestContext>(options =>
                    options.UseSqlite((SqliteConnection)Connection))
                .AddScoped<Repository<TestObject, TestContext>>()
                .AddScoped<Repository<RelatedTestObject, TestContext>>();

            return services.BuildServiceProvider();
        }

        private class TestContext : DbContext
        {
            public TestContext()
            {
            }

            public TestContext(DbContextOptions options) : base(options)
            {
            }

            public DbSet<TestObject> TestObjects { get; set; }

            protected override void OnModelCreating(ModelBuilder model)
            {
                model.Entity<RelatedTestObject>()
                    .HasOne(t => t.TestObject)
                    .WithMany(t => t.RelatedTestObjects);
            }
        }

        private class TestObject
        {
            public TestObject()
            {
                RelatedTestObjects = new HashSet<RelatedTestObject>();
            }

            public int Id { get; set; }
            public string Name { get; set; }

            public ICollection<RelatedTestObject> RelatedTestObjects { get; }
        }

        private class RelatedTestObject
        {
            public int Id { get; set; }
            public int TestObjectId { get; set; }
            public string Name { get; set; }

            public TestObject TestObject { get; set; }
        }

        [Fact]
        public async void Create_record_async_test()
        {
            try
            {
                var services = GetServiceProvider();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                await services.GetService<TestContext>().Database.EnsureCreatedAsync();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                await repository.CreateAsync(testObject);

                Assert.Equal(1, repository.Entities.Count());
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void Create_record_test()
        {
            try
            {
                var services = GetServiceProvider();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                services.GetService<TestContext>().Database.EnsureCreated();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                repository.Create(testObject);
                Assert.Equal(1, repository.Entities.Count());
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public async void delete_record_async_test()
        {
            try
            {
                var services = GetServiceProvider();

                await services.GetService<TestContext>().Database.EnsureCreatedAsync();
                var repository = services.GetService<Repository<TestObject, TestContext>>();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                await repository.CreateAsync(testObject);
                Assert.Equal(1, repository.Entities.Count());

                await repository.DeleteAsync(testObject);
                Assert.Equal(0, repository.Entities.Count());
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void delete_record_test()
        {
            try
            {
                var services = GetServiceProvider();

                services.GetService<TestContext>().Database.EnsureCreatedAsync();
                var repository = services.GetService<Repository<TestObject, TestContext>>();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                repository.Create(testObject);
                Assert.Equal(1, repository.Entities.Count());

                repository.Delete(testObject);
                Assert.Equal(0, repository.Entities.Count());
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public async void Find_by_async_test()
        {
            try
            {
                var services = GetServiceProvider();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                await services.GetService<TestContext>().Database.EnsureCreatedAsync();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                await repository.CreateAsync(testObject);


                var foundObjects = await repository.FindByAsync(t => t.Name == "Test1");
                var foundObject = foundObjects.FirstOrDefault();

                Debug.Assert(foundObject != null, nameof(foundObject) + " != null");
                Assert.Equal(1, foundObject.Id);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void Find_by_test()
        {
            try
            {
                var services = GetServiceProvider();

                services.GetService<TestContext>().Database.EnsureCreated();
                var repository = services.GetService<Repository<TestObject, TestContext>>();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                repository.Create(testObject);

                var foundObject = repository.FindBy(t => t.Name == "Test1").FirstOrDefault();

                Debug.Assert(foundObject != null, nameof(foundObject) + " != null");
                Assert.Equal(1, foundObject.Id);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void find_record_by_criteria_with_include_test()
        {
            try
            {
                var services = GetServiceProvider();

                services.GetService<TestContext>().Database.EnsureCreated();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                var relatedRepository = services.GetService<Repository<RelatedTestObject, TestContext>>();

                var testObject1 = new TestObject { Id = 1, Name = "Test5" };
                var testObject2 = new TestObject { Id = 2, Name = "Test4" };
                var testObject3 = new TestObject { Id = 3, Name = "Test3" };
                var testObject4 = new TestObject { Id = 4, Name = "Test2" };
                var testObject5 = new TestObject { Id = 5, Name = "Test1" };

                repository.Create(testObject1);
                repository.Create(testObject2);
                repository.Create(testObject3);
                repository.Create(testObject4);
                repository.Create(testObject5);

                var relatedTestObject1 = new RelatedTestObject { Id = 1, TestObjectId = 5, Name = "RelatedTest1" };
                var relatedTestObject2 = new RelatedTestObject { Id = 2, TestObjectId = 5, Name = "RelatedTest2" };
                var relatedTestObject3 = new RelatedTestObject { Id = 3, TestObjectId = 5, Name = "RelatedTest3" };
                var relatedTestObject4 = new RelatedTestObject { Id = 4, TestObjectId = 4, Name = "RelatedTest1" };
                var relatedTestObject5 = new RelatedTestObject { Id = 5, TestObjectId = 4, Name = "RelatedTest2" };
                var relatedTestObject6 = new RelatedTestObject { Id = 6, TestObjectId = 4, Name = "RelatedTest3" };

                relatedRepository.Create(relatedTestObject1);
                relatedRepository.Create(relatedTestObject2);
                relatedRepository.Create(relatedTestObject3);
                relatedRepository.Create(relatedTestObject4);
                relatedRepository.Create(relatedTestObject5);
                relatedRepository.Create(relatedTestObject6);

                var foundObjects = repository.FindBy(
                    t => t.Name.Contains("Test"),
                    b => b.RelatedTestObjects);

                Assert.Equal(5, foundObjects.Count());
                Assert.Equal("Test5", foundObjects.ElementAt(0).Name);
                Assert.Equal("Test4", foundObjects.ElementAt(1).Name);
                Assert.Equal("Test3", foundObjects.ElementAt(2).Name);
                Assert.Equal("Test2", foundObjects.ElementAt(3).Name);
                Assert.Equal("Test1", foundObjects.ElementAt(4).Name);

                Assert.Equal("RelatedTest1", foundObjects.ElementAt(3).RelatedTestObjects.ElementAt(0).Name);
                Assert.Equal("RelatedTest2", foundObjects.ElementAt(3).RelatedTestObjects.ElementAt(1).Name);
                Assert.Equal("RelatedTest3", foundObjects.ElementAt(3).RelatedTestObjects.ElementAt(2).Name);
                Assert.Equal("RelatedTest1", foundObjects.ElementAt(4).RelatedTestObjects.ElementAt(0).Name);
                Assert.Equal("RelatedTest2", foundObjects.ElementAt(4).RelatedTestObjects.ElementAt(1).Name);
                Assert.Equal("RelatedTest3", foundObjects.ElementAt(4).RelatedTestObjects.ElementAt(2).Name);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void find_record_by_criteria_with_max_records_and_orderby_and_include_test()
        {
            try
            {
                var services = GetServiceProvider();

                services.GetService<TestContext>().Database.EnsureCreated();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                var relatedRepository = services.GetService<Repository<RelatedTestObject, TestContext>>();

                var testObject1 = new TestObject { Id = 1, Name = "Test5" };
                var testObject2 = new TestObject { Id = 2, Name = "Test4" };
                var testObject3 = new TestObject { Id = 3, Name = "Test3" };
                var testObject4 = new TestObject { Id = 4, Name = "Test2" };
                var testObject5 = new TestObject { Id = 5, Name = "Test1" };

                repository.Create(testObject1);
                repository.Create(testObject2);
                repository.Create(testObject3);
                repository.Create(testObject4);
                repository.Create(testObject5);

                var relatedTestObject1 = new RelatedTestObject { Id = 1, TestObjectId = 5, Name = "RelatedTest1" };
                var relatedTestObject2 = new RelatedTestObject { Id = 2, TestObjectId = 5, Name = "RelatedTest2" };
                var relatedTestObject3 = new RelatedTestObject { Id = 3, TestObjectId = 5, Name = "RelatedTest3" };
                var relatedTestObject4 = new RelatedTestObject { Id = 4, TestObjectId = 4, Name = "RelatedTest1" };
                var relatedTestObject5 = new RelatedTestObject { Id = 5, TestObjectId = 4, Name = "RelatedTest2" };
                var relatedTestObject6 = new RelatedTestObject { Id = 6, TestObjectId = 4, Name = "RelatedTest3" };

                relatedRepository.Create(relatedTestObject1);
                relatedRepository.Create(relatedTestObject2);
                relatedRepository.Create(relatedTestObject3);
                relatedRepository.Create(relatedTestObject4);
                relatedRepository.Create(relatedTestObject5);
                relatedRepository.Create(relatedTestObject6);

                var foundObjects = repository.FindBy(
                    t => t.Name.Contains("Test"),
                    l => l.OrderBy(k => k.Name),
                    3,
                    b => b.RelatedTestObjects);

                Assert.Equal(3, foundObjects.Count());
                Assert.Equal("Test1", foundObjects.ElementAt(0).Name);
                Assert.Equal("Test2", foundObjects.ElementAt(1).Name);
                Assert.Equal("Test3", foundObjects.ElementAt(2).Name);
                Assert.Equal("RelatedTest1", foundObjects.ElementAt(0).RelatedTestObjects.ElementAt(0).Name);
                Assert.Equal("RelatedTest2", foundObjects.ElementAt(0).RelatedTestObjects.ElementAt(1).Name);
                Assert.Equal("RelatedTest3", foundObjects.ElementAt(0).RelatedTestObjects.ElementAt(2).Name);
                Assert.Equal("RelatedTest1", foundObjects.ElementAt(1).RelatedTestObjects.ElementAt(0).Name);
                Assert.Equal("RelatedTest2", foundObjects.ElementAt(1).RelatedTestObjects.ElementAt(1).Name);
                Assert.Equal("RelatedTest3", foundObjects.ElementAt(1).RelatedTestObjects.ElementAt(2).Name);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void find_record_by_criteria_with_max_records_and_orderby_test()
        {
            try
            {
                var services = GetServiceProvider();

                services.GetService<TestContext>().Database.EnsureCreated();
                var repository = services.GetService<Repository<TestObject, TestContext>>();

                var testObject1 = new TestObject { Id = 1, Name = "Test5" };
                var testObject2 = new TestObject { Id = 2, Name = "Test4" };
                var testObject3 = new TestObject { Id = 3, Name = "Test3" };
                var testObject4 = new TestObject { Id = 4, Name = "Test2" };
                var testObject5 = new TestObject { Id = 5, Name = "Test1" };

                repository.Create(testObject1);
                repository.Create(testObject2);
                repository.Create(testObject3);
                repository.Create(testObject4);
                repository.Create(testObject5);

                var foundObjects = repository.FindBy(
                    t => t.Name.Contains("Test"),
                    l => l.OrderBy(k => k.Name),
                    3);

                Assert.Equal(3, foundObjects.Count());
                Assert.Equal("Test1", foundObjects.ElementAt(0).Name);
                Assert.Equal("Test2", foundObjects.ElementAt(1).Name);
                Assert.Equal("Test3", foundObjects.ElementAt(2).Name);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void find_record_by_criteria_with_max_records_test()
        {
            try
            {
                var services = GetServiceProvider();

                services.GetService<TestContext>().Database.EnsureCreated();
                var repository = services.GetService<Repository<TestObject, TestContext>>();

                var testObject1 = new TestObject { Id = 1, Name = "Test1" };
                var testObject2 = new TestObject { Id = 2, Name = "Test2" };
                var testObject3 = new TestObject { Id = 3, Name = "Test3" };
                var testObject4 = new TestObject { Id = 4, Name = "Test4" };
                var testObject5 = new TestObject { Id = 5, Name = "Test5" };

                repository.Create(testObject1);
                repository.Create(testObject2);
                repository.Create(testObject3);
                repository.Create(testObject4);
                repository.Create(testObject5);

                var foundObjects = repository.FindBy(t => t.Name.Contains("Test"), 3);

                Assert.Equal(3, foundObjects.Count());
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void find_record_by_criteria_with_orderby_and_include_test()
        {
            try
            {
                var services = GetServiceProvider();

                services.GetService<TestContext>().Database.EnsureCreated();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                var relatedRepository = services.GetService<Repository<RelatedTestObject, TestContext>>();

                var testObject1 = new TestObject { Id = 1, Name = "Test5" };
                var testObject2 = new TestObject { Id = 2, Name = "Test4" };
                var testObject3 = new TestObject { Id = 3, Name = "Test3" };
                var testObject4 = new TestObject { Id = 4, Name = "Test2" };
                var testObject5 = new TestObject { Id = 5, Name = "Test1" };

                repository.Create(testObject1);
                repository.Create(testObject2);
                repository.Create(testObject3);
                repository.Create(testObject4);
                repository.Create(testObject5);

                var relatedTestObject1 = new RelatedTestObject { Id = 1, TestObjectId = 5, Name = "RelatedTest1" };
                var relatedTestObject2 = new RelatedTestObject { Id = 2, TestObjectId = 5, Name = "RelatedTest2" };
                var relatedTestObject3 = new RelatedTestObject { Id = 3, TestObjectId = 5, Name = "RelatedTest3" };
                var relatedTestObject4 = new RelatedTestObject { Id = 4, TestObjectId = 4, Name = "RelatedTest1" };
                var relatedTestObject5 = new RelatedTestObject { Id = 5, TestObjectId = 4, Name = "RelatedTest2" };
                var relatedTestObject6 = new RelatedTestObject { Id = 6, TestObjectId = 4, Name = "RelatedTest3" };

                relatedRepository.Create(relatedTestObject1);
                relatedRepository.Create(relatedTestObject2);
                relatedRepository.Create(relatedTestObject3);
                relatedRepository.Create(relatedTestObject4);
                relatedRepository.Create(relatedTestObject5);
                relatedRepository.Create(relatedTestObject6);

                var foundObjects = repository.FindBy(
                    t => t.Name.Contains("Test"),
                    l => l.OrderBy(k => k.Name),
                    b => b.RelatedTestObjects);

                Assert.Equal(5, foundObjects.Count());
                Assert.Equal("Test1", foundObjects.ElementAt(0).Name);
                Assert.Equal("Test2", foundObjects.ElementAt(1).Name);
                Assert.Equal("Test3", foundObjects.ElementAt(2).Name);
                Assert.Equal("Test4", foundObjects.ElementAt(3).Name);
                Assert.Equal("RelatedTest1", foundObjects.ElementAt(0).RelatedTestObjects.ElementAt(0).Name);
                Assert.Equal("RelatedTest2", foundObjects.ElementAt(0).RelatedTestObjects.ElementAt(1).Name);
                Assert.Equal("RelatedTest3", foundObjects.ElementAt(0).RelatedTestObjects.ElementAt(2).Name);
                Assert.Equal("RelatedTest1", foundObjects.ElementAt(1).RelatedTestObjects.ElementAt(0).Name);
                Assert.Equal("RelatedTest2", foundObjects.ElementAt(1).RelatedTestObjects.ElementAt(1).Name);
                Assert.Equal("RelatedTest3", foundObjects.ElementAt(1).RelatedTestObjects.ElementAt(2).Name);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void find_record_by_key_test()
        {
            try
            {
                var services = GetServiceProvider();
                services.GetService<TestContext>().Database.EnsureCreated();
                var repository = services.GetService<Repository<TestObject, TestContext>>();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                repository.Create(testObject);

                var foundObject = repository.FindByKey(1);

                Assert.Equal(1, foundObject.Id);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public async void update_record_async_test()
        {
            try
            {
                var services = GetServiceProvider();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                await services.GetService<TestContext>().Database.EnsureCreatedAsync();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                var result = await repository.CreateAsync(testObject);

                if (result.Succeeded)
                {
                    testObject.Name = "Test2";
                    await repository.UpdateAsync(testObject);
                }

                Assert.Equal(1, repository.Entities.Count());
                Assert.Equal("Test2", repository.Entities.FirstOrDefault().Name);
            }
            finally
            {
                Connection.Close();
            }
        }

        [Fact]
        public void update_record_test()
        {
            try
            {
                var services = GetServiceProvider();
                var repository = services.GetService<Repository<TestObject, TestContext>>();
                services.GetService<TestContext>().Database.EnsureCreated();

                var testObject = new TestObject { Id = 1, Name = "Test1" };

                repository.Create(testObject);

                testObject.Name = "Test2";

                repository.Update(testObject);

                Assert.Equal(1, repository.Entities.Count());
                Assert.Equal("Test2", repository.Entities.FirstOrDefault().Name);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}