using System.Data.SQLite;
using System.Reflection;
using TodosApp.DB.Models;

namespace TodosApp.DB.Services;

public abstract class BaseService<T>
where T: BaseModel, new()
{
    public readonly string TableName;
    private readonly DbContext _context = DbContext.Instance;
    private readonly Dictionary<string, Func<object>> _defaultValues = new Dictionary<string, Func<object>>()
    {
        {"CreatedAt", () => DateTime.Now as object},
        {"UpdatedAt", () => DateTime.Now as object},
    };

    private FieldInfo[] Fields => typeof(T).GetFields().Where(field => field.IsPublic).ToArray();

    protected BaseService(string tableName)
    {
        TableName = tableName;
    }
    
    protected BaseService(string tableName, Dictionary<string, Func<object>> defaultValues)
    {
        TableName = tableName;
        defaultValues.ToList().ForEach(x => _defaultValues.Add(x.Key, x.Value));
    }

    public List<T> Select(SQLiteCommand command)
    {
        var result = new List<T>();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var row = _parse(reader);

            result.Add(row);
        }

        return result;
    }

    public List<T> Select(string sql)
    {
        var command = CreateCommand(sql);
        return Select(command);
    }
    
    public T GetItemById(int id)
    {
        var command = CreateCommand(AppendTableName("SELECT * FROM {0} WHERE Id = @id"));
        command.Parameters.AddWithValue("@id", id.ToString());

        var resultList = Select(command);

        if (resultList.Count == 0)
        {
            throw new KeyNotFoundException();
        }
        
        return resultList[0];
    }

    public void Add(T data)
    {
        SetDefaultValues(data);
        
        var sql = SqlBuilder.BuildInsert(TableName, data);
        var command = CreateCommand(sql);

        command.ExecuteNonQuery();
    }

    public string AppendTableName(string sql)
    {
        return string.Format(sql, TableName);
    }

    public SQLiteCommand CreateCommand(string sql)
    {
        _context.OpenConnectionIfClosed();

        return new SQLiteCommand(sql, _context.Connection);
    }
    
    private T _parse(SQLiteDataReader reader)
    {
        var result = new T();

       try
       {
           foreach (var field in Fields)
           {
               field.SetValue(result, reader[field.Name]);
           }
       }
       catch (Exception e)
       {
           Console.WriteLine(e);
           throw e;
       }
        

        return result;
    }

    private void SetDefaultValues(T row)
    {
        foreach (var entry in _defaultValues)
        {
            var field = Fields.First(field => field.Name == entry.Key);

            if (field == null)
            {
                continue;;
            }
            
            field.SetValue(row, entry.Value());
        }
    }
}