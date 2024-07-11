namespace DataMapping;

public class GenericDatabaseItem
{
    public Dictionary<string, object?> Properties { get; set; } = new Dictionary<string, object?>();

    public void SetProperty(string key, object? value)
    {
        Properties[key.ToLower()] = value;
    }

    public object GetProperty(string key)
    {
        if (Properties.ContainsKey(key))
        {
            return Properties[key];
        }

        throw new Exception($"Property {key} not found.");
    }
    
    public string GenerateInsertStatement(string tableName)
    {
        var columns = string.Join(", ", Properties.Keys);
        var values = string.Join(", ", Properties.Values.Select(v => GenericSqlFormatter.FormatSqlValue(v)));
        return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
    }
}