using CryptoCurrencyQuery.Application.Common.Mappings;
using CryptoCurrencyQuery.Domain.Entities;

namespace CryptoCurrencyQuery.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
