using Microsoft.AspNetCore.Mvc;
using resturangAPI_MVC.Models;
using System.Diagnostics;
using System.Net.Http;

namespace resturangAPI_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;


        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task <IActionResult> Index()
        {
            var response = _httpClient.GetAsync("api/menus");
            var menuList = await response.Result.Content.ReadFromJsonAsync<List<Menu>>();
            return View(menuList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

            // You can log the exception here
            Console.WriteLine($"Global error: {feature?.Error}");

            // Show a user-friendly page
            return View(new ErrorViewModel
            {
                Message = "Something went wrong. Please try again later.",
                Path = HttpContext.Request.Path
            });
        }
    }
}
