namespace TodosApp.Localization;

public class Localization
{
    private readonly Dictionary<string, Locale> _localesCache = new Dictionary<string, Locale>();
    private Locale? _currentLocale;
    private string _fallbackLocaleName = "en-US";

    private Locale _locale
    {
        get
        {
            if (_currentLocale == null)
            {
                var locale = GetLocalByKey(_fallbackLocaleName);

                _currentLocale = locale;

                return locale;
            }
            
            return _currentLocale;
        }
    }

    public Localization(string localeName)
    {
        SetLocale(localeName);
    }
    
    public void SetLocale(string localeName)
    {
        _currentLocale = GetLocalByKey(localeName);
    }

    public Locale GetLocalByKey(string localeName)
    {
        try
        {
            return _localesCache[localeName];
        }
        catch
        {
            return LoadAndCacheLocale(localeName);
        }
    }

    public string Get(string key)
    {
        return _locale.GetByKey(key);
    }

    private Locale LoadAndCacheLocale(string localeName)
    {
        var locale = new Locale();

        locale.Load(localeName);
        _localesCache[localeName] = locale;
        
        return locale;
    }
}