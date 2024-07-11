using System.Data.SqlClient;

namespace DataMapping;

public class GenericDataEditor
{
    public static void EditData(List<GenericDatabaseItem> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            Console.WriteLine($"Item {i + 1}:");
            foreach (var property in data[i].Properties)
            {
                Console.WriteLine($"{property.Key}: {property.Value}");
            }

            Console.WriteLine("Do you want to edit any properties? (yes/no)");
            string editResponse = Console.ReadLine();
            if (editResponse.ToLower() == "yes")
            {
                Console.WriteLine("Enter the key of the property you want to edit:");
                string key = Console.ReadLine();
                Console.WriteLine("Enter the new value for the property:");
                string newValue = Console.ReadLine();
                data[i].SetProperty(key, newValue);
            }
        }
    }
}