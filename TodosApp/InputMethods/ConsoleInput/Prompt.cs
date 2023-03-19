using Spectre.Console;
namespace TodosApp.InputMethods;

public class Prompt
{
    public Localization.Localization t;
    
    public Prompt(Localization.Localization localization)
    {
        t = localization;
    }
    
    public string Ask(string key)
    {
        var result = FormattedAsk(key);
        
        while (result == null || result.Trim().Length == 0)
        {
            var errorMessage = t.Get($"prompt.{key}.error");
            
            AnsiConsole.Write(new Markup($"[bold red]{errorMessage}[/]\n") );
            
            result = FormattedAsk(key);
        }

        return result;
    }
    
    public string? AskSoft(string promptKey)
    {
        var result = FormattedAsk(promptKey);

        return result;
    }

    private string? FormattedAsk(string key)
    {
        return AnsiConsole.Prompt(new TextPrompt<string?>(t.Get($"prompt.{key}.question")).PromptStyle("blue").AllowEmpty());
    }
}