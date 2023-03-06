using TodosApp.DB.Models;
using TodosApp.DB.Services;

namespace TodosApp.InputMethods.ConsoleInput.PromptHandlers;

public class AddTodoHandler: BasePromptHandler
{
    private Prompt _prompt;
    
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
        
            Console.Write(prompt.t.Get("addTodo.success"));
        }
        catch (Exception e)
        {
            Console.Write(prompt.t.Get("addTodo.error"));
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
        model.Completed = 0;
        model.Description = description;
        model.Task = task;

        return model;
    }

    private bool IsOwnCommand(string message)
    {
        return message.ToLower().Contains("add");
    }
}