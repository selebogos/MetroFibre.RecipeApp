// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;


List<PreparationRequest> i = new List<PreparationRequest>
            {
                new PreparationRequest{ Name = "cucumber", Quantity = 2 },
                new  PreparationRequest{ Name = "olives", Quantity = 2 },
                new  PreparationRequest{ Name = "lettuce", Quantity = 3 },
                new  PreparationRequest{ Name = "meat", Quantity = 6 },
                new  PreparationRequest{ Name = "tomato", Quantity = 6 },
                new  PreparationRequest{ Name = "cheese", Quantity = 8 },
                new  PreparationRequest{ Name = "dough", Quantity = 10 },
            };

using (var httpClient = new HttpClient())
{
    httpClient.BaseAddress = new Uri("https://localhost:7283");
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/FoodCreator/");
    string incredients = JsonConvert.SerializeObject(i);
    var formData = new Dictionary<string, string>
                {
                    { "incredients", incredients}
                };
     //request.Content = new FormUrlEncodedContent(formData);
    //request.Content = new StringContent(incredients, Encoding.UTF8, "application/json");

    var request = new HttpRequestMessage(HttpMethod.Post, "api/FoodCreator/")
    {
        Content = new StringContent(incredients, Encoding.UTF8, "application/json") // Ensure Content-Type is application/json
    };

    var response = await httpClient.SendAsync(request);
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception($"Cannot get the results, status code: {response.StatusCode}");
    }
    else 
    {
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response Content:");
        Console.WriteLine(content);
    }
 
}
Console.Read();


public class PreparationRequest
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}