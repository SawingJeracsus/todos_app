namespace Program;

public abstract class BaseInputMethod
{
    private InputBus _bus;

    protected InputBus Bus
    {
        get
        {
            return _bus;
        }
    }
    public BaseInputMethod(InputBus bus)
    {
        _bus = bus;
    }

    public abstract void Listen();
}