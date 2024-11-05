using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class GamesController : Controller
{
    private readonly HttpClient _httpClient;

    public GamesController()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.freetogame.com/api/games")
        };
    }

    [HttpGet]
    public async Task<IActionResult> AllGames()
    {
        try
        {

            // Fetch the JSON data from the API
            var response = await _httpClient.GetAsync("games");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                // Deserialize to List<GameList>
                var games = JsonSerializer.Deserialize<List<GameList>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Pass the list to the view
                return View(games);
            }

        }
        catch (Exception ex) { 
         return View(new List<GameList>());
        
        }
        return View(new List<GameList>()); // return an empty list or an error view
        // Handle the case when the API response is unsuccessful
    }
}



//1. way to consuming api when modal is created 

//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Text.Json;

//public class GamesController : Controller
//{
//    private readonly HttpClient _httpClient;

//    public GamesController()
//    {
//        _httpClient = new HttpClient
//        {
//            BaseAddress = new Uri("https://randomuser.me/api/")
//        };
//    }

//    [HttpGet]
//    public async Task<IActionResult> Index()
//    {
//        // Send GET request to fetch the data
//        var response = await _httpClient.GetAsync("");

//        // Check if the response was successful
//        if (response.IsSuccessStatusCode)
//        {
//            // Read and deserialize the JSON response
//            var jsonData = await response.Content.ReadAsStringAsync();
//            var result = JsonSerializer.Deserialize<GameList>(jsonData, new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });

//            // Pass the data to the view
//            return View(result);
//        }

//        // If the response failed, return an error view or message
//        return View("Error");
//    }
//}

//2.way When modal is not created using Dynamic keyword

//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Text.Json;

//public class GamesController : Controller
//{
//    private readonly HttpClient _httpClient;

//    public GamesController()
//    {
//        _httpClient = new HttpClient
//        {
//            BaseAddress = new Uri("https://randomuser.me/api/")
//        };
//    }

//    [HttpGet]
//    public async Task<IActionResult> Index()
//    {
//        var response = await _httpClient.GetAsync("");

//        if (response.IsSuccessStatusCode)
//        {
//            var jsonData = await response.Content.ReadAsStringAsync();

//            // Deserialize JSON into a dynamic object
//            dynamic result = JsonSerializer.Deserialize<dynamic>(jsonData);

//            // Pass the dynamic result to the view
//            return View(result);
//        }

//        return View("Error");
//    }
//}


//3.way Using JsonDocument for Accessing Data without a Model

//using Microsoft.AspNetCore.Mvc;
//using System.Runtime.Intrinsics.X86;
//using System.Text.Json;

//public class GamesController : Controller
//{
//    private readonly HttpClient _httpClient;

//    public GamesController()
//    {
//        _httpClient = new HttpClient
//        {
//            BaseAddress = new Uri("https://randomuser.me/api/")
//        };
//    }

//    [HttpGet]
//    public async Task<IActionResult> Index()
//    {
//        var response = await _httpClient.GetAsync("");
//        if (response.IsSuccessStatusCode)
//        {
//            using var jsonDocument = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
//            var users = jsonDocument.RootElement
//                                    .GetProperty("results")
//                                    .EnumerateArray()
//                                    .Select(user => JsonSerializer.Deserialize<Dictionary<string, object>>(user.ToString()))
//                                    .Where(user => user != null)
//                                    .Select(user => new Dictionary<string, object>
//                                    {
//                                        ["name"] = user.ContainsKey("name") && user["name"] is JsonElement nameElem ? JsonSerializer.Deserialize<Dictionary<string, object>>(nameElem.ToString()) : new Dictionary<string, object>(),
//                                        ["location"] = user.ContainsKey("location") && user["location"] is JsonElement locElem ? JsonSerializer.Deserialize<Dictionary<string, object>>(locElem.ToString()) : new Dictionary<string, object>(),
//                                        ["picture"] = user.ContainsKey("picture") && user["picture"] is JsonElement picElem ? JsonSerializer.Deserialize<Dictionary<string, object>>(picElem.ToString()) : new Dictionary<string, object>(),
//                                        ["email"] = user.ContainsKey("email") ? user["email"] : string.Empty
//                                    })
//                                    .ToList();

//            return View(users);
//        }

//        return View(new List<Dictionary<string, object>>());
//    }
//}


//1.Use Models: For known and stable data structures. (Best and recommended approach)
//2.Use JsonDocument: For performance-sensitive scenarios or highly dynamic data.
//3.Use dynamic: Sparingly and only for quick prototypes or very dynamic responses.

//other way

//Anonymous Types: for quick, localized data handling.
//Third-Party Libraries: like Newtonsoft.Json for additional flexibility.
//Custom Deserialization Logic: for complex data needs.
//Data Transfer Objects (DTOs): for decoupling and simplifying data structures.