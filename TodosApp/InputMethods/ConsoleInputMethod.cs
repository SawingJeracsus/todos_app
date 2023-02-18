namespace TodosApp.InputMethods;

public class ConsoleInputMethod : BaseInputMethod
{
    public ConsoleInputMethod(InputBus bus) : base(bus) {}

    public override void Listen()
    {
        // todo make session logic (with FileService)
        // todo move it to 118n service
        Console.WriteLine("Welcome to the ToDo's App. Please, provide your login for me:");
        var login = _prompt("Welcome to the ToDo's App. Please, provide your login for me:", "Login can't be empty!");
        var nickname = _prompt("Please, provide your nickname for me:", "Nickname can't be empty!");

        Bus.Start(login, nickname);
        
        Console.WriteLine("Welcome to mine prompt! to get commands list just type help");
        _startPrompt();
    }

    private string? _prompt(string promptQuestion)
    {
        Console.WriteLine(promptQuestion);
        var result = Console.ReadLine();

        return result;
    }

    private string _prompt(string promptQuestion, string errorMessage)
    {
        Console.WriteLine(promptQuestion);
        var result = Console.ReadLine();
        
        while (result == null)
        {
            Console.WriteLine(errorMessage);
            
            result = Console.ReadLine();
        }

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