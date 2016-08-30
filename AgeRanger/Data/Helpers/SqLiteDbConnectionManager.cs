using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace AgeRanger.Data.Helpers
{
    public class SqLiteDbConnectionManager : IDbConnectionManager
    {
        public static string DbFile => AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["db-file-name"];

        public DbConnection DbConnection()
        {
            if (!File.Exists(DbFile))
            {
                throw new Exception("DB file is not available");
            }

            return new SQLiteConnection("Data Source=" + DbFile);
        }
        
    }
}