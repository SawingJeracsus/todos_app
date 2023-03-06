using TodosApp.DB.Models;

namespace TodosApp.DB.Services;

public class TodoService: BaseService<TodoModel>
{
    public TodoService() : base("todo")
    { }
}