using TodosApp.InputMethods.PromptHandlers;

namespace TodosApp.InputMethods.ConsoleInput;

public class ConsoleInputMethod : BaseInputMethod
{
    // t means translation, it is often-used short name
    private Localization.Localization t;
    private Prompt _prompt;
    
    public ConsoleInputMethod(InputBus bus, Localization.Localization localization) : base(bus)
    {
        t = localization;
        _prompt = new Prompt(t);
    }

    public override void Listen()
    {
        
        Console.WriteLine(t.Get("welcome"));
        
        if (Bus.ShouldRegisterUser())
        {
            var login = _prompt.Ask("login");
            var nickname = _prompt.Ask("nickname");

            Bus.CreateUser(login, nickname);    
        }
        
        Console.WriteLine(t.Get("prompt.welcome"));
        _startPrompt();
    }

    private void _startPrompt()
    {
        var handlerFabric = new PromptHandlerFabric();
        
        while (true)
        {
            var input = Console.ReadLine();

            if (input == null || input.Trim().Length == 0)
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