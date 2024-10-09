using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

/*
 * The purpose of the code is to retrieve and display weather information for various European cities from a website.
 * The console app shows the weather data for each city is structured in a table where the city name and temperature are 
 * positioned in a predictable manner relative to each other.
 */

/*
 * Code snippet from Postman:
 * // Create an HttpClient instance to send an HTTP GET request.
 * var client = new HttpClient();
 * // Define the URL for the weather webpage.
 * var request = new HttpRequestMessage(HttpMethod.Get, "https://www.timeanddate.no/vaer/?continent=europe&low=c");
 * // Send the GET request and get the response.
 * var response = await client.SendAsync(request);
 * response.EnsureSuccessStatusCode();
 * // Extract the response content (HTML).
 * Console.WriteLine(await response.Content.ReadAsStringAsync());
 */


// Prints a greeting message to the console
Console.WriteLine("Hi, I will be your weatherbunny today!");

// Defines a URL for a weather information page for European cities
var url = "https://www.timeanddate.no/vaer/?continent=europe&low=c";

// Creates an instance of HtmlWeb to load and parse HTML from a web page
var web = new HtmlWeb();

// Loads the HTML content from the specified URL into a 'dom' object
var dom = web.Load(url);

// Selects all <td> (table data) nodes from the HTML document
var cities = dom.DocumentNode.SelectNodes("//td");

// Iterates over the selected <td> nodes in steps of 4 (assuming weather data is in a tabular format)
for (int index = 0; index < cities.Count; index += 4)
{
    // Extracts the temperature information from the <td> node (assumed to be at index + 3)
    string temperature = cities[index + 3].InnerText;

    // Finds the position of the '&' character in the temperature string
    int posAmbersand = temperature.IndexOf('&');

    // Prints the city name (at index) and the temperature, removing the last 6 characters of the temperature string (to remove '&nbsp')
    Console.WriteLine(cities[index].InnerText + " " + cities[index + 3].InnerText.Remove(posAmbersand, 6));
}