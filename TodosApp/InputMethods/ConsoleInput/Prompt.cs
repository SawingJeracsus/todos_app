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
        Console.WriteLine(t.Get($"prompt.{key}.question"));
        var result = Console.ReadLine();
        
        while (result == null)
        {
            Console.WriteLine(t.Get($"prompt.{key}.error"));
            
            result = Console.ReadLine();
        }

        return result;
    }
    
    public string? AskSoft(string promptKey)
    {
        Console.WriteLine(t.Get($"prompt.{promptKey}.question"));
        var result = Console.ReadLine();

        return result;
    }
}