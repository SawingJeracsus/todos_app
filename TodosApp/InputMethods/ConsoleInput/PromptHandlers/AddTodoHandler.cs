using Spectre.Console;
using TodosApp.DB.Models;
using TodosApp.DB.Services;

namespace TodosApp.InputMethods.ConsoleInput.PromptHandlers;

public class AddTodoHandler: BasePromptHandler
{
    private Prompt _prompt;

    public override string GetCommandName()
    {
        return "add";
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
            var model = PromptModel();
        
            service.Add(model);
        
            AnsiConsole.MarkupLine($"[bold green]{prompt.t.Get("addTodo.success")}[/]");
        }
        catch
        {
            AnsiConsole.MarkupLine($"[bold red]{prompt.t.Get("addTodo.error")}[/]");
        }
        
        
        return true;
    }

    private TodoModel PromptModel()
    {
        var user = UserService.GetSessionUser();

        if (user == null)
        {
            throw new Exception("No use in session");
        }

        var task = _prompt.Ask("todoTitle");
        var description = _prompt.AskSoft("todoDescription");

        var model = new TodoModel();

        model.Author = user.Id;
        model.Assignee = user.Id;
        model.Completed = false;
        model.Description = description;
        model.Task = task;

        return model;
    }

    private bool IsOwnCommand(string message)
    {
        return message.ToLower().Contains("add");
    }
}