using System.Collections.Generic;
using System.Linq;
using AgeRanger.Data.Helpers;
using Dapper;

namespace AgeRanger.Data.Repositories
{
    public class SqLitePersonRepository : IPersonRepository
    {
        private readonly IDbConnectionManager connectionManager;

        public SqLitePersonRepository(IDbConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public Person Insert(Person person)
        {
            using (var cnn = connectionManager.DbConnection())
            {
                cnn.Open();
                person.Id = cnn.Query<long>(
                    @"INSERT INTO Person 
                    ( FirstName, LastName, Age) VALUES 
                    ( @FirstName, @LastName, @Age );
                    SELECT last_insert_rowid()", person).First();

                return person;
            }
        }

        public void Update(Person person)
        {
            using (var cnn = connectionManager.DbConnection())
            {
                cnn.Open();
                cnn.Query(
                    @"Update Person 
                    SET FirstName = @FirstName, LastName = @LastName, Age = @Age
                    WHERE Id = @Id;", person);
            }
        }

        public void Delete(Person person)
        {
            using (var cnn = connectionManager.DbConnection())
            {
                cnn.Open();
                cnn.Query(
                    @"DELETE FROM Person 
                    WHERE Id = @Id;", person);
            }
        }

        public IEnumerable<Person> GetAll()
        {
            using (var cnn = connectionManager.DbConnection())
            {
                cnn.Open();
                return cnn.Query<Person>(
                    @"SELECT Id, FirstName, LastName, Age
                      FROM Person");
            }
        }

        public Person Get(long id)
        {
            using (var cnn = connectionManager.DbConnection())
            {
                cnn.Open();
                var result = cnn.Query<Person>(
                    @"SELECT Id, FirstName, LastName, Age
                    FROM Person
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Person> Search(string searchCondition)
        {
            using (var cnn = connectionManager.DbConnection())
            {
                cnn.Open();
                return cnn.Query<Person>(
                    @"SELECT Id, FirstName, LastName, Age
                      FROM Person
                      WHERE FirstName LIKE '%@searchCondition%' 
                            OR LastName LIKE '%@searchCondition%'", new { searchCondition });
            }
        }


        
    }
}