
namespace AgeRanger.Model.DTO
{
    /// <summary>
    /// DTO for Person object
    /// </summary>
    public class DetailedPerson
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long Age { get; set; }

        public string AgeGroup { get; set; }
    }
}