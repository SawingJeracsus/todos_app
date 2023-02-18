namespace TodosApp.InputMethods;

public class ConsoleInputMethod : BaseInputMethod
{
    // t means translation, it is often used short name
    private Localization.Localization t;
    
    public ConsoleInputMethod(InputBus bus, Localization.Localization localization) : base(bus)
    {
        t = localization;
    }

    public override void Listen()
    {
        // todo make session logic (with FileService)
        Console.WriteLine(t.Get("welcome"));
        var login = _prompt("login");
        var nickname = _prompt("nickname");

        Bus.Start(login, nickname);
        
        Console.WriteLine("Welcome to mine prompt! to get commands list just type help");
        _startPrompt();
    }

    private string _prompt(string promptKey)
    {
        Console.WriteLine(t.Get($"prompt.{promptKey}.question"));
        var result = Console.ReadLine();
        
        while (result == null)
        {
            Console.WriteLine(t.Get($"prompt.{promptKey}.error"));
            
            result = Console.ReadLine();
        }

        return result;
    }
    
    private string? _promptSoft(string promptKey)
    {
        Console.WriteLine(t.Get($"prompt.{promptKey}.question"));
        var result = Console.ReadLine();

        return result;
    }

    private void _startPrompt()
    {
        while (true)
        {
            var input = Console.ReadLine();
            
            Console.WriteLine(input);
        }
    }
}