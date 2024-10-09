using Exercise_Cars_Dapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Exercise_Cars_Dapper.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Cars> cars { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=CarsDatabase;User ID=sa;Password=SecretPassword01;Encrypt=False;";
            
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                // Query to fetch the list of cars
                string query = "SELECT LicensePlate, Make, Model, Color FROM Cars"; // Adjust the query to match your table schema
                cars = db.Query<Cars>(query); // Dapper will map the results to the Car model
            }
        }
    }
}
