using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TodosApp.Localization;

public class Locale
{
    private dynamic _localeData;
    
    public void Load(string locale)
    {
        var path = GetLocalPath(locale);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        using (var reader = new StreamReader(path))
        {
            var jsonContent = reader.ReadToEnd();
            var localeContent = JsonConvert.DeserializeObject<dynamic>(jsonContent);
            
            if (localeContent == null)
            {
                throw new FileLoadException();
            }

            _localeData = localeContent;
        }
    }

    public string GetByKey(string key)
    {
        var keyParts = key.Split('.');

        try
        {
            var result = _localeData;

            foreach (var nestedKey in keyParts)
            {
                result = result[nestedKey];
            }
            
            if(result is JValue)
            {
                return result.ToString();
            }
            
            throw new KeyNotFoundException();
        }
        catch
        {
            return key;
        }
    }

    private string GetLocalPath(string locale)
    {
        return Path.Combine(Environment.CurrentDirectory, $"Localization/locales/{locale}.json");
    }
}