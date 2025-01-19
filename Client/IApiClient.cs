using Contracts;
using Refit;

namespace Client;

public interface IApiClient
{
    [Get("/todos")]
    Task<Todo[]> GetTodos();

    [Get("/todos/{id}")]
    Task<Todo> GetTodoById(int id);
}