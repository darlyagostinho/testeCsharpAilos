using Microsoft.Data.Sqlite;

namespace Questao5.Infrastructure.Repository
{
    public class SqLiteRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\database.sqlite"; }
        }

        public static SqliteConnection DbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
