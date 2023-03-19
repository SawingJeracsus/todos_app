using Spectre.Console;
using TodosApp.DB.Models;
using TodosApp.DB.Services;

namespace TodosApp.InputMethods.ConsoleInput.PromptHandlers;

public class CompleteTodoHandler: BasePromptHandler
{
    private Prompt _prompt;

    public override string GetCommandName()
    {
        return "cmpl";
    }

    public override bool OnMessage(string message, Prompt prompt)
    {
        _prompt = prompt;
        
        if (!IsOwnCommand(message))
        {
            return false;
        }

        try
        {
            var service = new TodoService();
            var todos = GetAssignedTodos();

            if (todos.Length != 0)
            {
                var todo = AnsiConsole.Prompt(GetSelectionPrompt(todos));
                service.MarkAsCompleted(todo.Id);
        
                AnsiConsole.Write(new Markup($"[bold green]{_prompt.t.Get("todo.complete.success")}[/]\n"));    
            }
            else
            {
                AnsiConsole.Write(new Markup($"[bold yellow]{_prompt.t.Get("todo.noRows")}[/]\n"));
            }
        }
        catch
        {
            AnsiConsole.Write(new Markup($"[bold red]{_prompt.t.Get("todo.complete.error")}[/]\n"));
        }
        
        return true;
    }

    private bool IsOwnCommand(string message)
    {
        return message == "cmpl";
    }
    
    private TodoModel[] GetAssignedTodos()
    {
        var todoService = new TodoService();
        var user = UserService.GetSessionUser();

        if (user == null)
        {
            throw new Exception("No user in session");
        }

        return todoService.Select($"SELECT * FROM {todoService.TableName} WHERE Assignee = {user.Id} AND Completed = 0").ToArray();
    }

    private SelectionPrompt<TodoModel> GetSelectionPrompt(TodoModel[] todos)
    {
        var select = new SelectionPrompt<TodoModel>()
            .Title(_prompt.t.Get("todo.complete.select"))
            .PageSize(10)
            .MoreChoicesText(_prompt.t.Get("todo.complete.moreOptions"))
            .AddChoices(todos);
        select.Converter = (todo) => todo.Task;

        return select;
    }
}