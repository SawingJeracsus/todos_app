using System.Data.SQLite;
using TodosApp.DB.Models;

namespace TodosApp.DB.Services;

public abstract class BaseService<T>
where T: BaseModel
{
    private readonly string _tableName;
    private readonly DbContext _context = DbContext.Instance;

    public BaseService(string tableName)
    {
        _tableName = tableName;
    }

    
    public abstract T Parse(SQLiteDataReader reader);

    protected BaseModel GetBaseModel(SQLiteDataReader reader)
    {
        var result = new BaseModel();

        result.Id = (int) reader["Id"];
        result.CreatedAt = DateTime.Parse((string) reader["created_at"]);
        result.UpdatedAt = DateTime.Parse((string) reader["updated_at"]);
        
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

    private string _appendTableName(string sql)
    {
        return string.Format(sql, _tableName);
    }
}