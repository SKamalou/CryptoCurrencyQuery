using CryptoCurrencyQuery.Application.TodoLists.Queries.ExportTodos;

namespace CryptoCurrencyQuery.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
