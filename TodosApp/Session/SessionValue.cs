using Newtonsoft.Json;

namespace TodosApp.Session;

public abstract class SessionValue
{
    private readonly string _key;

    private string _path
    {
        get
        {
            return Path.Combine(Environment.CurrentDirectory, $"Session/session.json");
        }
    }
    
    protected SessionValue(string key)
    {
        _key = key;
    }

    public void Set(string value)
    {
        var prevDictionary = ReadJson();
        prevDictionary[_key] = value;
        WriteJson(prevDictionary);
    }

    public string? Get()
    {
        try
        {
            var dictionary = ReadJson();
            
            return dictionary[_key];
        }
        catch
        {
            return null;
        }
    }

    private Dictionary<string, string> ReadJson()
    {
        CheckFileExists();

        using var reader = new StreamReader(_path);
        var content = reader.ReadToEnd();
            
        var session = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

        if (session == null)
        {
            return new Dictionary<string, string>();
        }

        return session;
    }

    private void WriteJson(Dictionary<string, string> value)
    {
        CheckFileExists();

        using var writer = new StreamWriter(_path);
        var jsonContent = JsonConvert.SerializeObject(value);
            
        writer.Write(jsonContent);
    }

    private void CheckFileExists()
    {
        if (!File.Exists(_path))
        {
            throw new FileNotFoundException();
        }
    }
}