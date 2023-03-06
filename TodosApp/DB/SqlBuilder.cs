namespace TodosApp.DB;

public class SqlBuilder
{
    public static String BuildInsert(String tableName, object data)
    {
        var fields = data.GetType().GetFields();
        string[] defaultColumns = { "Id" };
        
        var columns = fields.Where(field => field.IsPublic && defaultColumns.All(defaultColumn => defaultColumn != field.Name )).ToArray();
        var columnsLine = String.Join(",", columns.Select(column => column.Name));
        var values = new List<String>();

        foreach (var column in columns)
        {
            values.Add(EscapeValue(column.GetValue(data)));
        }

        return $@"INSERT INTO {tableName} ({columnsLine}) VALUES ({String.Join(",", values)});";
    }
    
    public static String BuildInsert(String tableName, object[] data)
    {
        var queries = new List<String>();

        foreach (var dataItem in data)
        {
            queries.Add(BuildInsert(tableName, data));
        }
        
        return String.Join("\n", queries);
    }
    public static String EscapeValue(object? value)
    {
        var nullString = "NULL";
        
        if (value == null)
        {
            return nullString;
        }

        var type = value.GetType().Name;
        Console.Write(type);
        
        switch (value.GetType().Name)
        {
            case "String":
                return $"\"{value}\"";
            case "Int32":
            case "Int64":
            case "Double":
                var result = value.ToString();
                
                if (result == null)
                {
                    return nullString;
                }
                
                return result;
            case "DateTime":
                var dateTime = (DateTime) value;
                
                return $"\"{dateTime.Year.ToString()}-{dateTime.Month.ToString("00")}-{dateTime.Day.ToString("00")} {dateTime.Hour.ToString("00")}:{dateTime.Minute.ToString("00")}:{dateTime.Millisecond.ToString("00")}\"";
            default:
                return nullString;
        }
    }
}