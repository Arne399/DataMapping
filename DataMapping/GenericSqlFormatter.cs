namespace DataMapping;

public class GenericSqlFormatter
{
    public static string? FormatSqlValue(object? value)
    {
        if (value == null)
        {
            return "NULL";
        }

        return value switch
        {
            string stringValue => $"'{stringValue}'",
            DateTime dateTimeValue => $"'{dateTimeValue:yyyy-MM-dd HH:mm:ss}'",
            bool boolValue => boolValue ? "1" : "0",
            int intValue => intValue.ToString(),
            _ => value.ToString()
        };
    }
}