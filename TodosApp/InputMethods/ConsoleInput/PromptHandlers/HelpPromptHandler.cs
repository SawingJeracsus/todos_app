using Spectre.Console;
using TodosApp.InputMethods.PromptHandlers;

namespace TodosApp.InputMethods.ConsoleInputMethod.PromptHandlers;

public class HelpPromptHandler: BasePromptHandler
{
    private Prompt _prompt;
    
    public override string GetCommandName()
    {
        return "help";
    }

    public override bool OnMessage(string message, Prompt prompt)
    {
        _prompt = prompt;
        
        if (!IsOwnCommand(message))
        {
            return false;
        }

        var table = InitTable();
        var commands = PromptHandlerFabric.GetCommandNames();
        
        foreach (var command in commands)
        {
            table.AddRow(
                new Markup($"[bold blue]{command}[/]"), 
                new Markup($"[yellow]{prompt.t.Get($"help.rows.{command}")}[/]")
                );
        }
        
        AnsiConsole.Write(table);
        
        return true;
    }

    private bool IsOwnCommand(string message)
    {
        return message.ToLower().Contains("help");
    }

    private Table InitTable()
    {
        var table = new Table();

        table.AddColumn(_prompt.t.Get("help.columns.name"));
        table.AddColumn(_prompt.t.Get("help.columns.description"));
        table.Border(TableBorder.Rounded);
        
        return table;
    }
}