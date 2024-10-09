/*
 * API WE HAVE ACCESS TO
 */

// Create a WhoisLookup instance
using Whois;

var whois1 = new WhoisLookup();
var whois2 = new WhoisLookup();
var whois3 = new WhoisLookup();

// Query github.com
var response1 = whois1.Lookup("github.com");
var response2 = whois2.Lookup("google.com");
var response3 = whois3.Lookup("ucn.dk");

// Output the response
Console.WriteLine(response1.Content);
Console.WriteLine(response2.Content);
Console.WriteLine(response3.Content);

// Domain Name: github.com
// Registry Domain ID: 1264983250_DOMAIN_COM-VRSN
// Registrar WHOIS Server: whois.markmonitor.com
// Registrar URL: http://www.markmonitor.com
// ...