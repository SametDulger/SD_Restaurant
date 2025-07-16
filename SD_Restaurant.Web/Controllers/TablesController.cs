using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace SD_Restaurant.Web.Controllers
{
    public class TablesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TablesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync("api/tables");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tables = JsonSerializer.Deserialize<List<TableViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(tables);
            }
            return View(new List<TableViewModel>());
        }

        public async Task<IActionResult> ByStatus(string status)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/tables/status/{Uri.EscapeDataString(status)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tables = JsonSerializer.Deserialize<List<TableViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.Status = status;
                return View("Index", tables);
            }
            return View("Index", new List<TableViewModel>());
        }

        public async Task<IActionResult> ByLocation(string location)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/tables/location/{Uri.EscapeDataString(location)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tables = JsonSerializer.Deserialize<List<TableViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.Location = location;
                return View("Index", tables);
            }
            return View("Index", new List<TableViewModel>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TableViewModel table)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(table);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/tables", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(table);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/tables/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var table = JsonSerializer.Deserialize<TableViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(table);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TableViewModel table)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(table);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"api/tables/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(table);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/tables/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var table = JsonSerializer.Deserialize<TableViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(table);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.DeleteAsync($"api/tables/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/tables/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var table = JsonSerializer.Deserialize<TableViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(table);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var content = new StringContent($"\"{status}\"", System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"api/tables/{id}/status", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Masa durumu başarıyla güncellendi.";
            }
            else
            {
                TempData["Error"] = "Masa durumu güncellenemedi.";
            }
            return RedirectToAction(nameof(Details), new { id });
        }
    }
} 