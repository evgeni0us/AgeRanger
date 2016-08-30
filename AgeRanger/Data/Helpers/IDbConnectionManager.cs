using System.Data.Common;

namespace AgeRanger.Data.Helpers
{
    /// <summary>
    /// Declares connection manager
    /// </summary>
    public interface IDbConnectionManager
    {
        DbConnection DbConnection();
    }
}