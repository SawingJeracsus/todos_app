using TodosApp.InputMethods.ConsoleInput.PromptHandlers;
using TodosApp.InputMethods.ConsoleInputMethod.PromptHandlers;

namespace TodosApp.InputMethods.PromptHandlers;

public class PromptHandlerFabric : BasePromptHandler
{
    private static readonly BasePromptHandler[] _methods =
    {
        new HelpPromptHandler(),
        new AddTodoHandler(),
        new GetTodosListHandler(),
        new CompleteTodoHandler(),
        new ExitHandler()
    };

    public static string[] GetCommandNames()
    {
        return _methods.Select(handler => handler.GetCommandName()).ToArray();
    }
    public override string GetCommandName()
    {
        return "fabric";
    }

    public override bool OnMessage(string message, Prompt prompt)
    {
        var wasHandled = false;
        
        foreach (var method in _methods)
        {
            wasHandled = method.OnMessage(message, prompt);

            if (wasHandled)
            {
                break;
            }
        }

        return wasHandled;
    }
}