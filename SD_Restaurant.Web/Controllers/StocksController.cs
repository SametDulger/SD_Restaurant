using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace SD_Restaurant.Web.Controllers
{
    public class StocksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StocksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync("api/stocks");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stocks = JsonSerializer.Deserialize<List<StockViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(stocks);
            }
            return View(new List<StockViewModel>());
        }

        public async Task<IActionResult> ByLocation(string location)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/stocks/location/{Uri.EscapeDataString(location)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stocks = JsonSerializer.Deserialize<List<StockViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.Location = location;
                return View("Index", stocks);
            }
            return View("Index", new List<StockViewModel>());
        }

        public async Task<IActionResult> ByProduct(int productId)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/stocks/product/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stocks = JsonSerializer.Deserialize<List<StockViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.ProductId = productId;
                return View("Index", stocks);
            }
            return View("Index", new List<StockViewModel>());
        }

        public async Task<IActionResult> LowStock()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync("api/stocks/low-stock");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stocks = JsonSerializer.Deserialize<List<StockViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.Title = "Düşük Stok Ürünleri";
                return View("Index", stocks);
            }
            return View("Index", new List<StockViewModel>());
        }

        public async Task<IActionResult> CheckAvailability(int productId, string location, decimal quantity)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/stocks/check-availability?productId={productId}&location={Uri.EscapeDataString(location)}&quantity={quantity}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var isAvailable = JsonSerializer.Deserialize<bool>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.ProductId = productId;
                ViewBag.Location = location;
                ViewBag.Quantity = quantity;
                ViewBag.IsAvailable = isAvailable;
                return View();
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StockViewModel stock)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(stock);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/stocks", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(stock);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/stocks/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stock = JsonSerializer.Deserialize<StockViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(stock);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StockViewModel stock)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(stock);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"api/stocks/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(stock);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/stocks/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stock = JsonSerializer.Deserialize<StockViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(stock);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.DeleteAsync($"api/stocks/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/stocks/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stock = JsonSerializer.Deserialize<StockViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(stock);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, string location, decimal quantity)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.PutAsync($"api/stocks/update-quantity?productId={productId}&location={Uri.EscapeDataString(location)}&quantity={quantity}", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Stok miktarı başarıyla güncellendi.";
            }
            else
            {
                TempData["Error"] = "Stok miktarı güncellenemedi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 