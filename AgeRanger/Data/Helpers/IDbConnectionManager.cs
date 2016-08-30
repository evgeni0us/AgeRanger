using System.Data.Common;

namespace AgeRanger.Data.Helpers
{
    public interface IDbConnectionManager
    {
        DbConnection DbConnection();
    }
}