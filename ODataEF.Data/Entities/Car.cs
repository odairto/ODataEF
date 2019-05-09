namespace ODataEF.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}

