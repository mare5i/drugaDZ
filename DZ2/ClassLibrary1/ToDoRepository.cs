using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly List<TodoItem> _inMemoryTodoDatabase;
        TodoItem pom;
        public TodoRepository(List<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new List<TodoItem>();
            }
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.FirstOrDefault(s => s.Id == todoId);
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            if (_inMemoryTodoDatabase.Count(s => s.Id == todoItem.Id) == 0)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
                throw new DuplicateTodoItemException($"duplicate id:{todoItem.Id}");
            }
        }

        public bool Remove(Guid todoId)
        {
            return _inMemoryTodoDatabase.Remove(Get(todoId));
        }

        public void Update(TodoItem todoItem)
        {
            pom = Get(todoItem.Id);
            if (pom != null)
            {
                _inMemoryTodoDatabase.Remove(pom);
            }
            Add(todoItem);
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            pom = Get(todoId);
            if (pom != null)
            {
                _inMemoryTodoDatabase.Remove(pom);
                pom.MarkAsCompleted();
                Add(pom);
                return true;
            }
            return false;
        }

        public List<TodoItem> GetAll()
        {
            List<TodoItem> lista = _inMemoryTodoDatabase.OrderByDescending(s => s.DateCreated).ToList();
            return lista;
        }

        public List<TodoItem> GetActive()
        {
            List<TodoItem> lista = _inMemoryTodoDatabase.Where(s => s.IsCompleted == false).ToList();
            return lista;
        }

        public List<TodoItem> GetCompleted()
        {
            List<TodoItem> lista = _inMemoryTodoDatabase.Where(s => s.IsCompleted == true).ToList();
            return lista;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            if (filterFunction != null)
            {
                List<TodoItem> lista = _inMemoryTodoDatabase.Where(s => filterFunction(s)).ToList();
                return lista;
            }
            throw new ArgumentNullException();
        }
    }
}
