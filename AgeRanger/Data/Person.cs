namespace AgeRanger.Data
{

    /// <summary>
    /// Person object represents record of Person table in RangeRanger.db
    /// </summary>
    public class Person
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long Age { get; set; }

    }
}