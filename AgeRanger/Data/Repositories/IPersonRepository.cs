using System.Collections.Generic;

namespace AgeRanger.Data.Repositories
{
    public interface IPersonRepository
    {
        Person Insert(Person person);

        void Update(Person person);

        void Delete(Person person);

        Person Get(long id);

        IEnumerable<Person> GetAll();

        IEnumerable<Person> Search(string searchCondition);

    }
}
