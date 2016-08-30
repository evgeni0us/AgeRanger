using System.Collections.Generic;

namespace AgeRanger.Data.Repositories
{
    /// <summary>
    /// Declares simple data access logic for AgeGroup table
    /// </summary>
    public interface IAgeGroupRepository
    {
        IEnumerable<AgeGroup> GetAll();
    }
}
