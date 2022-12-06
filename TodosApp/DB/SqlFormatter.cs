namespace TodosApp.DB;

public class SqlFormatter
{
    public static String BuildInsert(String tableName, object data)
    {
        var fields = data.GetType().GetFields();
        var columns = fields.Where(field => field.IsPublic && field.Name != "Id").ToArray();
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
        
        switch (value.GetType().Name)
        {
            case "String":
                return $"\"{value}\"";
            case "Int32":
            case "Double":
                var result = value.ToString();
                
                if (result == null)
                {
                    return nullString;
                }
                
                return result;
            default:
                return nullString;
        }
    }
}