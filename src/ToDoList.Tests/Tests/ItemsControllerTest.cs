using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Controllers;
using ToDoList.Models;
using ToDoList.Models.Repositories;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Tests
{
    public class ItemsControllerTest : IDisposable
    {
        EFItemRepository db = new EFItemRepository(new TestDbContext());

        [Fact]
        public void Test_of_the_Testing_Ability()
        {
            //Arrange
            ItemsController controller = new ItemsController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";

            // Act
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;

            // Assert
            Assert.Contains<Item>(testItem, collection);
        }
        public void Dispose()
        {
            db.DeleteAll();
        }
    }
}
