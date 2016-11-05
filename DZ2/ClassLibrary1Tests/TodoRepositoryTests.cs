using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }
        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }
        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }
        [TestMethod]
        public void RemovingExistingItemWillRemoveIt()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Remove(todoItem.Id) == true);
            Assert.IsTrue(repository.Get(todoItem.Id) == null);
            Assert.AreEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        public void RemovingItemWillReturnFalse()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            Assert.IsTrue(repository.Remove(todoItem.Id) == false);
        }

        [TestMethod]
        public void UpdatingNotExistingObject()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Update(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        public void MarkAsCompletedExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.IsTrue(!repository.Get(todoItem.Id).IsCompleted);
            Assert.IsTrue(repository.MarkAsCompleted(todoItem.Id));
            Assert.IsTrue(repository.Get(todoItem.Id).IsCompleted);
        }

        [TestMethod]
        public void MarkAsCompletedNotExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            Assert.IsTrue(repository.MarkAsCompleted(todoItem.Id) == false);
        }
        [TestMethod]
        public void GetAllSorted()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Notebooks ");
            List<TodoItem> a;
            repository.Add(todoItem);
            repository.Add(todoItem2);
            Assert.IsTrue(todoItem2.DateCreated >= todoItem.DateCreated);
            a = repository.GetAll();
            Assert.IsTrue(a[0].DateCreated >= a[1].DateCreated);
        }

        [TestMethod]
        public void GetAllCompleted()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Notebooks ");
            List<TodoItem> a;
            repository.Add(todoItem2);
            repository.Add(todoItem);
            repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(1, repository.GetCompleted().Count);
        }

        public void GetAllCompletedEmpty()
        {
            ITodoRepository repository = new TodoRepository();
            Assert.AreEqual(0, repository.GetActive().Count);
        }

        [TestMethod]
        public void GetAllActive()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Notebooks ");
            List<TodoItem> a;
            repository.Add(todoItem2);
            repository.Add(todoItem);
            repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(1, repository.GetActive().Count);
            Assert.IsTrue(repository.GetActive()[0].Id == todoItem2.Id);
        }
        [TestMethod]
        public void GetAllActiveEmpty()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Notebooks ");
            repository.Add(todoItem2);
            repository.Add(todoItem);
            repository.MarkAsCompleted(todoItem.Id);
            repository.MarkAsCompleted(todoItem2.Id);
            Assert.AreEqual(0, repository.GetActive().Count);
        }

        [TestMethod]
        public void Filtered()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Notebooks ");
            repository.Add(todoItem2);
            repository.Add(todoItem);
            repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(1, repository.GetFiltered(i => i.IsCompleted).Count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FilteredWithNull()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Notebooks ");
            repository.Add(todoItem2);
            repository.Add(todoItem);
            repository.MarkAsCompleted(todoItem.Id);
            repository.GetFiltered(null);
        }
        [TestMethod]
        public void FilteredWhenThereIsNoFilteredItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Notebooks ");
            repository.Add(todoItem2);
            repository.Add(todoItem);
            Assert.AreEqual(0, repository.GetFiltered(i => i.IsCompleted).Count);
        }


    }
}