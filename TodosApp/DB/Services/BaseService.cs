using System.Data.SQLite;
using System.Reflection;
using TodosApp.DB.Models;

namespace TodosApp.DB.Services;

public abstract class BaseService<T>
where T: BaseModel, new()
{
    private readonly string _tableName;
    private readonly DbContext _context = DbContext.Instance;

    private FieldInfo[] Fields => typeof(T).GetFields().Where(field => field.IsPublic).ToArray();

    public BaseService(string tableName)
    {
        _tableName = tableName;
    }

    private T Parse(SQLiteDataReader reader)
    {
        var result = new T();
        
        foreach (var field in Fields)
        {
            field.SetValue(result, reader[field.Name]);
        }

        return result;
    }
    public T GetItemById(int id)
    {
        var command = new SQLiteCommand(_appendTableName("SELECT * FROM {0} WHERE id = @id"), _context.Connection);
        command.Parameters.AddWithValue("@id", id.ToString());

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new KeyNotFoundException();
        }

        reader.Read();

        return Parse(reader);
    }

    public void Add(T data)
    {
        var sql = SqlFormatter.BuildInsert(_tableName, data);
        _context.OpenConnectionIfClosed();
        var command = new SQLiteCommand(sql, _context.Connection);

        command.ExecuteNonQuery();
    }

    private string _appendTableName(string sql)
    {
        return string.Format(sql, _tableName);
    }
}