// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

/*
 * This code sends a message to a private telegram channel. 
 * 
 * Code created by the program: postman
 */

var client = new HttpClient();
var request = new HttpRequestMessage(HttpMethod.Post, "https://api.telegram.org/bot7477200600:AAFhvKyepSWgxHiqrV0H2VRWVlXXNml1lU4/sendMessage");
var collection = new List<KeyValuePair<string, string>>();
collection.Add(new("chat_id", "-1002324491357"));
collection.Add(new("text", "hejsa med digsa, test from visual studio!!"));
var content = new FormUrlEncodedContent(collection);
request.Content = content;
var response = await client.SendAsync(request);
response.EnsureSuccessStatusCode();
Console.WriteLine(await response.Content.ReadAsStringAsync());
