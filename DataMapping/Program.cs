using DataMapping;

Database database = new Database("Server=localhost;Database=TestDatabase;User ID=sa;Password=SqlDb123;");

var testQuery = "SELECT * FROM TestTable WHERE Name = @Name;";
List<GenericDatabaseItem> data = database.RetrieveData(testQuery, new Dictionary<string, string> { { "Name", "Arne" } });

GenericDataEditor.EditData(data);

var filePath = Path.Combine(Environment.CurrentDirectory, "output.sql");
database.GenerateSqlFile(filePath, "TestTable", data);
