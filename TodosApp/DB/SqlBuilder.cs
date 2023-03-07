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
            values.Add(FormatsConverter.EscapeValue(column.GetValue(data)));
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
}