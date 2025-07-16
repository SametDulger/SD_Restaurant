using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace SD_Restaurant.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(products);
            }
            return View(new List<ProductViewModel>());
        }

        public async Task<IActionResult> Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return RedirectToAction(nameof(Index));
            }

            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/products/search?term={Uri.EscapeDataString(term)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.SearchTerm = term;
                return View("Index", products);
            }
            return View("Index", new List<ProductViewModel>());
        }

        public async Task<IActionResult> LowStock()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync("api/products/low-stock");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.Title = "Düşük Stok Ürünleri";
                return View("Index", products);
            }
            return View("Index", new List<ProductViewModel>());
        }

        public async Task<IActionResult> ByCategory(int categoryId)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/products/category/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View("Index", products);
            }
            return View("Index", new List<ProductViewModel>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/products", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<ProductViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"api/products/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<ProductViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(product);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.DeleteAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<ProductViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(product);
            }
            return NotFound();
        }
    }
} 