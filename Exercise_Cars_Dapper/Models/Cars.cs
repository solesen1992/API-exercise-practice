namespace Exercise_Cars_Dapper.Models
{
    /*
     * Class car goes with database table class
     */
    public class Cars
    {
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
    }
}
