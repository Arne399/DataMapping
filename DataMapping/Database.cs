using System.Data.SqlClient;

namespace DataMapping;

public class Database
{
    private readonly string _connectionString;
    private SqlConnection _connection;
    
    public Database(string connectionString)
    {
        _connectionString = connectionString;
        OpenConnection();
    }
    
    private void OpenConnection()
    {
        _connection = new SqlConnection(_connectionString);
        _connection.Open();
    }
    
    public List<GenericDatabaseItem> RetrieveData(string query, Dictionary<string, string> parameters)
    {
        var items = new List<GenericDatabaseItem>();
        
        using (var command = new SqlCommand(query, _connection))
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = new GenericDatabaseItem();
                    
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        item.SetProperty(reader.GetName(i), reader.GetValue(i));
                    }
                    
                    items.Add(item);
                }
            }
        }
        
        return items;
    }
    
    public void GenerateSqlFile(string filePath, string tableName, List<GenericDatabaseItem> items)
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var item in items)
            {
                var insertStatement = item.GenerateInsertStatement(tableName);
                writer.WriteLine(insertStatement);
            }
        }
    }
}