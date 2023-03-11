using TodosApp.DB.Models;

namespace TodosApp.DB.Services;

public class TodoService: BaseService<TodoModel>
{
    public TodoService() : base("todo", new Dictionary<string, Func<object>>()
    {
        {"Completed", () => false as object}
    })
    { }

    public void MarkAsCompleted(long Id)
    {
        var command = CreateCommand($"UPDATE {TableName} SET Completed = 1 WHERE Id = {Id}");
        command.ExecuteNonQuery();
    }
}