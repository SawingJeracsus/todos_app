namespace Program;

class Program
{
    static void Main()
    {
        var bus = new InputBus();
        var ioStream = new ConsoleInputMethod(bus);
        
        ioStream.Listen();
    }
}