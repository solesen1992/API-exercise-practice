using Exercise_Car_RazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Exercise_CarAPI.Controllers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Exercise_Car_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        // List to hold the cars fetched from the API
        [BindProperty]
        public List<Cars> Cars { get; set; } = new List<Cars>();

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient; // Inject HttpClient
        }

        /*
         * Get cars into table
         */
        public void OnGet()
        {

            using (HttpClient httpClient = new HttpClient()) {

                var response = _httpClient.GetAsync("http://localhost:5096/apiV1/cars").Result;
                Console.WriteLine("Response: " + response);

                // Ensure the call was succesful
                if (response.IsSuccessStatusCode)
                {
                    // Read response in JSON sring
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("jsonString: " + jsonString);

                    // Convert the JSON string to C# objects
                    Cars = JsonConvert.DeserializeObject<List<Cars>>(jsonString);
                    Console.WriteLine("Cars: " + Cars);
                }
            }
        }
    }
}