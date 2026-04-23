using System.Data.SQLite;
using System.IO;
using UnityEngine;

public class DBHelper : MonoBehaviour
{

    public static SQLiteConnection GetConnection()
    {
        string fileName = "ProgrammingTest.db";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string connectionString = $"Data Source={filePath}";

        SQLiteConnection connection = new SQLiteConnection(connectionString);

        return connection;
    }
}
