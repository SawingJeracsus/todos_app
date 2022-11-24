namespace Program;

public class ConsoleInputMethod : BaseInputMethod
{
    public ConsoleInputMethod(InputBus bus) : base(bus) {}

    public override void Listen()
    {
        // todo make session logic (with FileService)
        // todo move it to 118n service
        Console.WriteLine("Welcome to the ToDo's App. Please, provide your name for me:");
        var name = Console.ReadLine();

        while (name == null)
        {
            Console.WriteLine("Name can't be empty!");
            
            name = Console.ReadLine();
        }
        
        Bus.Start(name);
        
        Console.WriteLine("Welcome to mine prompt! to get commands list just type help");
        StartPrompt();
    }

    private void StartPrompt()
    {
        while (true)
        {
            var input = Console.ReadLine();
            
            Console.WriteLine(input);
        }
    }
}