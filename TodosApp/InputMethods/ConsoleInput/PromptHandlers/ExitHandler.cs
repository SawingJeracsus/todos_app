namespace TodosApp.InputMethods.ConsoleInput.PromptHandlers;

public class ExitHandler : BasePromptHandler
{
    public override string GetCommandName()
    {
        return "exit";
    }

    public override bool OnMessage(string message, Prompt prompt)
    {
        if (message == "exit")
        {
            Environment.Exit(0);
        }

        return true;
    }
}