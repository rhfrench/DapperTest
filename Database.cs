using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace DapperTests
{
    public static class Database
    {
        public static IDbConnection GetConnection()
        {
            File.Delete("test.db3");
            return new SqliteConnection("Data Source=test.db3");
        }
    }
}
