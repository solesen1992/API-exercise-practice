/*
 * VERSION 2
 * I've filled out the following methods so they're now working:
 * public void Post
 * public void Put
 * public void Delete
 * Added some come to the method Get_ThisNameDoesNotMatter(string? make = "%", string? model = "%", string? color = "%")
 */

// VERSION 1
// Author: Karsten Jeppesen, UCN, 2023
// CarAPI is used to demonstrate the potential of a REST API
// The exercise suggested is building a total API which may be accessed
// from:
//  - PostMan
//  - Swagger
//  - Razor based application
//
// Nuget Packages:
// - Dapper
// - System.Data.SqlClient
//
// NOTE: You must define the environment variable: ConnectionString
// NOTE: In the "Debug Launch Profile" you must change "App URL" to "http://localhost:15000"
//
// CarAPI-1: Adding the apiV1/Cars HttpGET action
// CarAPI-2: Adding the additional apiV2/Cars/AA 12345 HttpGET action
//           Adding the apiV1/Cars HttpPOST action 
// CarAPI-3: Adding filter to the apiV1/Cars HttpGET action
// CarAPI-4: Adding Razor UI as a multiproject solution

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Exercise_CarAPI.Model;
using Dapper;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exercise_CarAPI.Controllers
{
    [Route("apiV1/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private string createTable = "CREATE TABLE Cars (licenseplate VARCHAR(16),make VARCHAR(128),model VARCHAR(128),color VARCHAR(128),PRIMARY KEY (licenseplate))";
        // GET: api/Car
        /// <summary>
        /// Gets all cars in the table. Optional search arguments "make" and "color"
        /// </summary>
        /// <returns> List of Car</returns>
        //
        // Name of method is irrelevant. It is the routing that matters: [HttpGet]
        [HttpGet]
        public IActionResult Get_ThisNameDoesNotMatter(string? make = "%", string? model = "%", string? color = "%")
        {
            using (var connection = new SqlConnection("Data Source=localhost;Initial Catalog=CarsDatabase;User ID=sa;Password=SecretPassword01;Encrypt=False;"))
            {
                connection.Open();
                try
                {
                    /* 
                     * CHANGES ADDED TO CODE
                     * Return a List<Car>  
                     * Using a LIKE for at partial matching with wildcards
                     * The method allows for partial matches using wildcards (%) in the LIKE clause, 
                     * making it flexible for querying based on provided parameters.
                     */
                    var query = "SELECT * FROM dbo.Cars WHERE make LIKE @make AND model LIKE @model AND color LIKE @color";

                    // Pass the parameters correctly inside the Query method
                    var car = connection.Query<Cars>(query, new
                    {
                        make = make,
                        model = model,
                        color = color
                }).ToList();

                    return Ok(car);

                    //OLD CODE
                    // return Ok(connection.Query<Cars>("SELECT * FROM dbo.Cars").ToList());
                }
                catch
                {
                    // It doesn't exist - create and add two cars
                    connection.Execute(createTable);
                    connection.ExecuteScalar<Cars>("INSERT INTO dbo.Cars (licenseplate,make,model,color) VALUES (@licenseplate, @make, @model, @color)", new
                    {
                        licensplate = "AA 12345",
                        make = "Toyota",
                        model = "Yaris",
                        color = "Silver"
                    });
                    connection.ExecuteScalar<Cars>("INSERT INTO dbo.Cars (licenseplate,make,model,color) VALUES (@licenseplate, @make, @model, @color)", new
                    {
                        licensplate = "BB 12345",
                        make = "Ford",
                        model = "Ka",
                        color = "Red"
                    });
                }
                // Return a two item List
                List<Cars> cars = connection.Query<Cars>("SELECT * FROM dbo.Cars").ToList();
                return Ok(connection.Query<Cars>("SELECT * FROM dbo.Cars").ToList());
            }
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /* 
         * CHANGES ADDED TO CODE
         * Insert new car with information.
         */
        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] Cars newCar)
        {
            using (var connection = new SqlConnection(System.Environment.GetEnvironmentVariable("ConnectionString")))
            {
                connection.Open();
                string insertQuery = "INSERT INTO dbo.Cars (licenseplate, make, model, color) VALUES (@licenseplate, @make, @model, @color)";
                connection.Execute(insertQuery, new
                {
                    licensplate = newCar.LicensePlate,
                    make = newCar.Make,
                    model = newCar.Model,
                    color = newCar.Color
                });
            }
        }

        /* 
         * CHANGES ADDED TO CODE
         * Updating every information except the licenseplate.
         */
        // PUT api/<CarsController>/5
        [HttpPut("{licenseplate}")]
        public void Put(string licenseplate, [FromBody] Cars updatedCar)
        {
            using (var connection = new SqlConnection(System.Environment.GetEnvironmentVariable("ConnectionString")))
            {
                connection.Open();

                // Perform the update 
                string updateQuery = "UPDATE dbo.Cars SET make = @Make, model = @Model, color = @Color WHERE licenseplate = @Licenseplate";
                connection.Execute(updateQuery, new Cars
                {
                    LicensePlate = licenseplate,  // Use the provided license plate to find the car
                    Make = updatedCar.Make,   // Updated make from the request
                    Model = updatedCar.Model,  // Updated model from the request
                    Color = updatedCar.Color,  // Updated color from the request
                });
            }
        }

        /* 
         * CHANGES ADDED TO CODE
         * Deleting a car based on licenseplate.
         */
        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(string licenseplate)
        {
            using (var connection = new SqlConnection(System.Environment.GetEnvironmentVariable("ConnectionString")))
            {
                connection.Open();

                // Perform the delete
                string deleteQuery = "DELETE FROM dbo.Cars WHERE licenseplate = @Licenseplate";
                connection.Execute(deleteQuery, new { Licenseplate = licenseplate });
            }
        }
    }
}