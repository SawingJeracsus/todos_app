using TodosApp.DB.Models;
using TodosApp.DB.Services;

namespace TodosApp.InputMethods.ConsoleInput.PromptHandlers;

public class GetTodosListHandler : BasePromptHandler
{
    public override bool OnMessage(string message, Prompt prompt)
    {
        if (!isOwnComand(message))
        {
            return false;
        }

        var todos = GetAssignedTodos();
        PrintTodos(todos);
        
        return true;
    }

    private bool isOwnComand(string message)
    {
        return message == "ls";
    }

    private TodoModel[] GetAssignedTodos()
    {
        var todoService = new TodoService();
        var user = UserService.GetSessionUser();

        if (user == null)
        {
            throw new Exception("No use in session");
        }

        return todoService.Select($"SELECT * FROM {todoService.TableName} WHERE Assignee = {user.Id} AND Completed = 0").ToArray();
    }

    private string BuildStringFromTodo(TodoModel todo)
    {
        var dateString = $"{todo.CreatedAt.Date.Month}.{todo.CreatedAt.Date.Day}.{todo.CreatedAt.Date.Year}";

        var todoHeader = $"{todo.Task} {dateString}";

        if (todo.Description != null)
        {
            return $"{todoHeader}\n{todo.Description}";
        }

        return todoHeader;
    }

    private void PrintTodos(TodoModel[] todos)
    {
        var todosStrings = todos.Select(todo => BuildStringFromTodo(todo));

        Console.WriteLine(String.Join("\n\n", todosStrings));
    }
}