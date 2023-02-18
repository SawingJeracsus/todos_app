using TodosApp.InputMethods.PromptHandlers;

namespace TodosApp.InputMethods.ConsoleInput;

public class ConsoleInputMethod : BaseInputMethod
{
    // t means translation, it is often used short name
    private Localization.Localization t;
    private Prompt _prompt;
    
    public ConsoleInputMethod(InputBus bus, Localization.Localization localization) : base(bus)
    {
        t = localization;
        _prompt = new Prompt(t);
    }

    public override void Listen()
    {
        // todo make session logic (with FileService)
        Console.WriteLine(t.Get("welcome"));
        var login = _prompt.Ask("login");
        var nickname = _prompt.Ask("nickname");

        Bus.Start(login, nickname);
        
        Console.WriteLine("Welcome to mine prompt! to get commands list just type help");
        _startPrompt();
    }

    private void _startPrompt()
    {
        var handlerFabric = new PromptHandlerFabric();
        
        while (true)
        {
            var input = Console.ReadLine();

            if (input == null)
            {
                continue;;
            }

            var wasHandled = handlerFabric.OnMessage(input, _prompt);

            if (!wasHandled)
            {
                Console.WriteLine(t.Get("errors.unsupportedCommand"));
            }
        }
    }
}