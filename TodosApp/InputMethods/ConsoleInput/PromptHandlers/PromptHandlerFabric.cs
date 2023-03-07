using TodosApp.InputMethods.ConsoleInput.PromptHandlers;
using TodosApp.InputMethods.ConsoleInputMethod.PromptHandlers;

namespace TodosApp.InputMethods.PromptHandlers;

public class PromptHandlerFabric : BasePromptHandler
{
    private readonly BasePromptHandler[] _methods =
    {
        new HelpPromptHandler(),
        new AddTodoHandler(),
        new GetTodosListHandler()
    };

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