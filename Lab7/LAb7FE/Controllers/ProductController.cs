using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LAb7FE.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _iHttpClientFactory;
        private readonly IHttpContextAccessor _iAccessor;
        public ProductController(IHttpClientFactory iHttpClientFactory, IHttpContextAccessor iAccessor)
        {
            _iHttpClientFactory = iHttpClientFactory;
            _iAccessor = iAccessor;
        }
        public async Task<IActionResult> Index()
        {
            string authorize = _iAccessor.HttpContext.Request.Cookies["Authorize"];
            var response = await FetchAsync("https://localhost:7269/api/Product", string.Empty, string.Empty, "GET", "Bearer "+authorize.ToString());
            if (response.IsSuccessStatusCode)
            {
                var dataString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Product>>(dataString);
                return View(result);
            }
            return RedirectToAction("Login", "Home");
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var payload = JsonConvert.SerializeObject(product);
            string authorize = _iAccessor.HttpContext.Request.Cookies["Authorize"];
            var response = await FetchAsync("https://localhost:7269/api/Product", string.Empty, payload, "POST", "Bearer " + authorize.ToString());
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update()
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
