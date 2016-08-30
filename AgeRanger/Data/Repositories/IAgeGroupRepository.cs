using System.Collections.Generic;

namespace AgeRanger.Data.Repositories
{
    public interface IAgeGroupRepository
    {
        IEnumerable<AgeGroup> GetAll();
    }
}
