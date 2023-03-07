using System.Text.RegularExpressions;

namespace TodosApp.DB;

public class FormatsConverter
{
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

                return ConvertToSqliteDateTime(dateTime);
            case "Boolean":
                return (bool)value ? "1" : "0";
            default:
                return nullString;
        }
    }

    private static string ConvertToSqliteDateTime(DateTime dateTime)
    {
        var dayPart =
            $"{dateTime.Year.ToString()}-{dateTime.Month.ToString("00")}-{dateTime.Day.ToString("00")}";
        var secondsPart =
            $"{dateTime.Hour.ToString("00")}:{dateTime.Minute.ToString("00")}:{dateTime.Second.ToString("00")}";
                
        return $"\"{dayPart} {secondsPart}\"";
    }
}