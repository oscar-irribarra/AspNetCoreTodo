using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetCoreTodo.UnitTests
{
    public class TodoItemServiceShould
    {
        //Arrange: Inicializar Objetos
        //Act: Llamar metodo a validar
        //Assert: Validar

        //Equal(resultado esperado, variable)
        private DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                          .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;
        [Fact]
        public async Task AddNewItemAsIncompleteWithDueDate()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

                var fakeUser = new ApplicationUser
                {
                    Id = "fake-000",
                    UserName = "fake@fake.cl"
                };

                var fakeItem = new TodoItem
                {
                    Title = "Testing?",
                    DueAt = DateTimeOffset.Now.AddDays(0)
                };

                //Act
                await service.AddItemAsync(fakeItem, fakeUser);
                
                //Assert
                Assert.Equal(1, await context.Items.CountAsync());

                var item = await context.Items.FirstAsync();
                Assert.Equal("Testing?", item.Title);
                Assert.Equal(false, item.IsDone);
                Assert.True(DateTimeOffset.Now.AddDays(3) - item.DueAt > TimeSpan.FromSeconds(1));
            }
        }

        [Fact]
        public async Task ItemDoesntexist_RFalse()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

                var fakeUser = new ApplicationUser
                {
                    Id = "fake-001",
                    UserName = "fake@fake.cl"
                };

                var fakeItem = new TodoItem
                {
                    Title = "Testing?",
                    DueAt = DateTimeOffset.Now.AddDays(0)
                };

                await service.AddItemAsync(fakeItem, fakeUser);

                //Act
                var result = await service.MarkDoneAsync(new Guid(), fakeUser);

                //Assert
                Assert.Equal(false, result);
            }
        }

        [Fact]
        public async Task ReturnTrueItemDoesntexist_RTrue()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

                var fakeUser = new ApplicationUser
                {
                    Id = "fake-002",
                    UserName = "fake@fake.cl"
                };

                var fakeItem = new TodoItem
                {
                    Title = "Testing?",
                    DueAt = DateTimeOffset.Now.AddDays(0)
                };

                await service.AddItemAsync(fakeItem, fakeUser);

                //Act
                var result = await service.MarkDoneAsync(fakeItem.Id, fakeUser);

                //Assert
                Assert.Equal(true, result);
            }
        }
       
        [Fact]
        public async Task ReturnItemsOwnedByUser()
        {
            //Arrange
            using(var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

                var fakeUser = new ApplicationUser
                {
                    Id = "fake-003",
                    UserName = "fake@fake.cl"
                };

                var fakeItem = new TodoItem
                {
                    Title = "Testing?",
                    DueAt = DateTimeOffset.Now.AddDays(0)
                };

                await service.AddItemAsync(fakeItem, fakeUser);

                var fakeUser2 = new ApplicationUser
                {
                    Id = "fake-004",
                    UserName = "fake@fake.cl"
                };

                var fakeItem2 = new TodoItem
                {
                    Title = "Testing?",
                    DueAt = DateTimeOffset.Now.AddDays(0)
                };

                await service.AddItemAsync(fakeItem2, fakeUser2);

                //Act
                var listaItems = await service.GetIncompleteItemsAsync(fakeUser);
            
                //Assert
                Assert.Equal(1 , listaItems.Length);
            }
        }


    }
}