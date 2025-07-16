using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace SD_Restaurant.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync("api/customers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customers = JsonSerializer.Deserialize<List<CustomerViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(customers);
            }
            return View(new List<CustomerViewModel>());
        }

        public async Task<IActionResult> Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return RedirectToAction(nameof(Index));
            }

            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/customers/search?term={Uri.EscapeDataString(term)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customers = JsonSerializer.Deserialize<List<CustomerViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.SearchTerm = term;
                return View("Index", customers);
            }
            return View("Index", new List<CustomerViewModel>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(customer);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/customers", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customer = JsonSerializer.Deserialize<CustomerViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(customer);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(customer);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"api/customers/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customer = JsonSerializer.Deserialize<CustomerViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(customer);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.DeleteAsync($"api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customer = JsonSerializer.Deserialize<CustomerViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(customer);
            }
            return NotFound();
        }
    }
} 