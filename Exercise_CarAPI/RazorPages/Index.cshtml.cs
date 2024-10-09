using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Exercise_CarAPI.Controllers;
using System.Drawing;
using System.Reflection;
using System.Text.Json;
using Exercise_CarAPI.Model;

namespace Exercise_CarAPI.RazorPages
{
    public class IndexModel : PageModel
    {
        /*private readonly CarsController _carsController;

        public IndexModel(CarsController carsController)
        {
            _carsController = carsController;
        }

        public void OnGet(string? make = "%", string? model = "%", string? color = "%")
        {
            _carsController.Get_ThisNameDoesNotMatter(make, model, color);

        }*/

        private readonly HttpClient _httpClient;

        // List to hold the cars fetched from the API
        public List<Cars> Cars { get; set; } = new List<Cars>();

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync(string? make = "%", string? model = "%", string? color = "%")
        {
			var response = await _httpClient.GetAsync($"http://localhost:5096/apiV1/cars?make={make}&model={model}&color={color}");

			if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Cars = JsonSerializer.Deserialize<List<Cars>>(jsonResponse);
            }
            else
            {
                // Handle the error (e.g., log it, display a message, etc.)
            }
        }
    }
}
