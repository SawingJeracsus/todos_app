namespace TodosApp.InputMethods;

public abstract class BasePromptHandler
{
    public abstract bool OnMessage(string message, Prompt prompt);
}