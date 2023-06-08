using LAb7FE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
namespace LAb7FE.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _iHttpClientFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory iHttpClientFactory)
        {
            _logger = logger;
            _iHttpClientFactory = iHttpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDto input)
        {
            var payload = JsonConvert.SerializeObject(input);
            var response = await FetchAsync("https://localhost:7269/api/User", string.Empty, payload, "POST");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
            }
            return null;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<HttpResponseMessage> FetchAsync(string baseUrl, string operation, string payload, string method, string authorization = "")
        {
            HttpResponseMessage response = null;
            using (var client = _iHttpClientFactory.CreateClient("ApiClient"))
            using (var httpContent = new StringContent(payload, Encoding.UTF8, "application/json"))
            {
                if (!string.IsNullOrEmpty(authorization))
                {
                    client.DefaultRequestHeaders.Add("Authorization", authorization);
                }
                client.BaseAddress = new Uri(baseUrl);
                switch (method.ToUpper())
                {
                    case "GET":
                        response = await client.GetAsync(operation);
                        break;
                    case "POST":
                        response = await client.PostAsync(operation, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync(operation, httpContent);
                        break;
                    default:
                        break;
                };
            }
            if (response is { IsSuccessStatusCode: true })
            {
                response.EnsureSuccessStatusCode();
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}