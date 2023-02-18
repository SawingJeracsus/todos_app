using System.Globalization;
using TodosApp.InputMethods;

namespace TodosApp;

class Program
{
    static void Main()
    {
        var bus = new InputBus();
        var localization = new Localization.Localization(CultureInfo.InstalledUICulture.Name);
        var ioStream = new ConsoleInputMethod(bus, localization);

        ioStream.Listen();
    }
}