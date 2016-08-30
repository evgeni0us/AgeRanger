using System.Collections.Generic;
using AgeRanger.Data.Helpers;
using Dapper;

namespace AgeRanger.Data.Repositories
{
    public class SqLiteAgeGroupRepository : IAgeGroupRepository
    {

        private readonly IDbConnectionManager connectionManager;

        public SqLiteAgeGroupRepository(IDbConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public IEnumerable<AgeGroup> GetAll()
        {
            using (var cnn = connectionManager.DbConnection())
            {
                cnn.Open();
                return cnn.Query<AgeGroup>(
                    @"SELECT Id, MinAge, MaxAge, Description
                      FROM AgeGroup");
            }
        }
    }
}
