namespace AgeRanger.Data
{
    /// <summary>
    /// AgeGroup object represents record of AgeGroup table in RangeRanger.db
    /// </summary>
    public class AgeGroup
    {
        public long Id { get; set; }

        public long? MinAge { get; set; }

        public long? MaxAge { get; set; }

        public string Description { get; set; }


    }
}