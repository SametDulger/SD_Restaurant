using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace SD_Restaurant.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync("api/payments");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payments = JsonSerializer.Deserialize<List<PaymentViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(payments);
            }
            return View(new List<PaymentViewModel>());
        }

        public async Task<IActionResult> ByOrder(int orderId)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/payments/order/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payments = JsonSerializer.Deserialize<List<PaymentViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.OrderId = orderId;
                return View("Index", payments);
            }
            return View("Index", new List<PaymentViewModel>());
        }

        public async Task<IActionResult> ByMethod(string paymentMethod)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/payments/method/{Uri.EscapeDataString(paymentMethod)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payments = JsonSerializer.Deserialize<List<PaymentViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.PaymentMethod = paymentMethod;
                return View("Index", payments);
            }
            return View("Index", new List<PaymentViewModel>());
        }

        public async Task<IActionResult> ByDateRange(DateTime startDate, DateTime endDate)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/payments/date-range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payments = JsonSerializer.Deserialize<List<PaymentViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", payments);
            }
            return View("Index", new List<PaymentViewModel>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentViewModel payment)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(payment);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/payments", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(payment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/payments/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payment = JsonSerializer.Deserialize<PaymentViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(payment);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PaymentViewModel payment)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                var json = JsonSerializer.Serialize(payment);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"api/payments/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(payment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/payments/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payment = JsonSerializer.Deserialize<PaymentViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(payment);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.DeleteAsync($"api/payments/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.GetAsync($"api/payments/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payment = JsonSerializer.Deserialize<PaymentViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(payment);
            }
            return NotFound();
        }
    }
} 