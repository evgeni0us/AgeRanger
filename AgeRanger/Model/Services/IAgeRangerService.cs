using System.Collections.Generic;
using AgeRanger.Model.DTO;

namespace AgeRanger.Model.Services
{
    public interface IAgeRangerService
    {
        IEnumerable<DetailedPerson> GetAllPeople();

        IEnumerable<DetailedPerson> FindPeople(string searchCondition);

        DetailedPerson AddPerson(DetailedPerson detailedPerson);

        void UpdatePerson(DetailedPerson detailedPerson);

        void DeletePerson(long id);


        IEnumerable<AgeGroup> GetAllAgeGroups();
    }
}