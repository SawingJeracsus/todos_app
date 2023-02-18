namespace TodosApp.InputMethods.ConsoleInputMethod.PromptHandlers;

public class HelpPromptHandler: BasePromptHandler
{
    public override bool OnMessage(string message, Prompt prompt)
    {
        if (!IsOwnCommand(message))
        {
            return false;
        }

        Console.WriteLine(prompt.t.Get("help"));

        return true;
    }

    private bool IsOwnCommand(string message)
    {
        return message.ToLower().Contains("help");
    }
}