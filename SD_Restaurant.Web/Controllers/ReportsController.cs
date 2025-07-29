using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace SD_Restaurant.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Ana raporlar sayfası
        public IActionResult Index()
        {
            return View();
        }

        // Satış raporları
        public async Task<IActionResult> Sales()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            
            // Son 30 günlük satış verilerini al
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);
            
            var response = await httpClient.GetAsync($"orders?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<OrderViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var salesReport = new SalesReportViewModel
                {
                    Orders = apiResponse?.Data ?? new List<OrderViewModel>(),
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalRevenue = apiResponse?.Data?.Sum(o => o.TotalAmount) ?? 0,
                    TotalOrders = apiResponse?.Data?.Count ?? 0
                };
                
                return View(salesReport);
            }
            
            return View(new SalesReportViewModel
            {
                Orders = new List<OrderViewModel>(),
                StartDate = startDate,
                EndDate = endDate
            });
        }

        // Müşteri analizi
        public async Task<IActionResult> CustomerAnalysis()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            
            var response = await httpClient.GetAsync("customers");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<CustomerViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var customerReport = new CustomerAnalysisViewModel
                {
                    Customers = apiResponse?.Data ?? new List<CustomerViewModel>(),
                    TotalCustomers = apiResponse?.Data?.Count ?? 0,
                    NewCustomersThisMonth = apiResponse?.Data?.Count(c => c.CreatedDate >= DateTime.Now.AddDays(-30)) ?? 0
                };
                
                return View(customerReport);
            }
            
            return View(new CustomerAnalysisViewModel
            {
                Customers = new List<CustomerViewModel>()
            });
        }

        // Stok raporları
        public async Task<IActionResult> Stock()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            
            var response = await httpClient.GetAsync("stocks");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<StockViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var stockReport = new StockReportViewModel
                {
                    Stocks = apiResponse?.Data ?? new List<StockViewModel>(),
                    TotalItems = apiResponse?.Data?.Count ?? 0,
                    LowStockItems = apiResponse?.Data?.Count(s => s.Quantity <= s.MinimumStockLevel) ?? 0,
                    OutOfStockItems = apiResponse?.Data?.Count(s => s.Quantity == 0) ?? 0
                };
                
                return View(stockReport);
            }
            
            return View(new StockReportViewModel
            {
                Stocks = new List<StockViewModel>()
            });
        }

        // Rezervasyon raporları
        public async Task<IActionResult> Reservations()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            
            var response = await httpClient.GetAsync("reservations");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ReservationViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var reservationReport = new ReservationReportViewModel
                {
                    Reservations = apiResponse?.Data ?? new List<ReservationViewModel>(),
                    TotalReservations = apiResponse?.Data?.Count ?? 0,
                    TodayReservations = apiResponse?.Data?.Count(r => r.ReservationDate.Date == DateTime.Today) ?? 0,
                    ThisWeekReservations = apiResponse?.Data?.Count(r => r.ReservationDate >= DateTime.Now.AddDays(-7)) ?? 0
                };
                
                return View(reservationReport);
            }
            
            return View(new ReservationReportViewModel
            {
                Reservations = new List<ReservationViewModel>()
            });
        }

        // Rapor indirme
        public async Task<IActionResult> Download(string reportType, DateTime? startDate = null, DateTime? endDate = null)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            
            switch (reportType.ToLower())
            {
                case "sales":
                    return await DownloadSalesReport(httpClient, startDate, endDate);
                case "customers":
                    return await DownloadCustomerReport(httpClient);
                case "stock":
                    return await DownloadStockReport(httpClient);
                case "reservations":
                    return await DownloadReservationReport(httpClient, startDate, endDate);
                default:
                    return RedirectToAction("Index");
            }
        }

        private async Task<IActionResult> DownloadSalesReport(HttpClient httpClient, DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.Now.AddDays(-30);
            var end = endDate ?? DateTime.Now;
            
            var response = await httpClient.GetAsync($"orders?startDate={start:yyyy-MM-dd}&endDate={end:yyyy-MM-dd}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<OrderViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                // CSV formatında rapor oluştur
                var csvContent = "Sipariş ID,Tarih,Müşteri,Masa,Tutar,Durum\n";
                foreach (var order in apiResponse?.Data ?? new List<OrderViewModel>())
                {
                    csvContent += $"{order.Id},{order.OrderDate:yyyy-MM-dd HH:mm},{order.CustomerName},{order.TableNumber},{order.TotalAmount},{order.Status}\n";
                }
                
                var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
                return File(bytes, "text/csv", $"satis_raporu_{start:yyyyMMdd}_{end:yyyyMMdd}.csv");
            }
            
            return RedirectToAction("Sales");
        }

        private async Task<IActionResult> DownloadCustomerReport(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("customers");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<CustomerViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var csvContent = "Müşteri ID,Ad,Soyad,E-posta,Telefon,Tip,Kayıt Tarihi\n";
                foreach (var customer in apiResponse?.Data ?? new List<CustomerViewModel>())
                {
                    csvContent += $"{customer.Id},{customer.FirstName},{customer.LastName},{customer.Email},{customer.Phone},{customer.CustomerType},{customer.CreatedDate:yyyy-MM-dd}\n";
                }
                
                var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
                return File(bytes, "text/csv", $"musteri_raporu_{DateTime.Now:yyyyMMdd}.csv");
            }
            
            return RedirectToAction("CustomerAnalysis");
        }

        private async Task<IActionResult> DownloadStockReport(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("stocks");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<StockViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var csvContent = "Ürün ID,Ürün Adı,Miktar,Minimum Seviye,Konum,Son Güncelleme\n";
                foreach (var stock in apiResponse?.Data ?? new List<StockViewModel>())
                {
                    csvContent += $"{stock.ProductId},{stock.ProductName},{stock.Quantity},{stock.MinimumStockLevel},{stock.Location},{stock.LastUpdated:yyyy-MM-dd HH:mm}\n";
                }
                
                var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
                return File(bytes, "text/csv", $"stok_raporu_{DateTime.Now:yyyyMMdd}.csv");
            }
            
            return RedirectToAction("Stock");
        }

        private async Task<IActionResult> DownloadReservationReport(HttpClient httpClient, DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.Now.AddDays(-30);
            var end = endDate ?? DateTime.Now;
            
            var response = await httpClient.GetAsync($"reservations?startDate={start:yyyy-MM-dd}&endDate={end:yyyy-MM-dd}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ReservationViewModel>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var csvContent = "Rezervasyon ID,Müşteri,Masa,Tarih,Kişi Sayısı,Durum\n";
                foreach (var reservation in apiResponse?.Data ?? new List<ReservationViewModel>())
                {
                    csvContent += $"{reservation.Id},{reservation.CustomerName},{reservation.TableNumber},{reservation.ReservationDate:yyyy-MM-dd HH:mm},{reservation.NumberOfGuests},{reservation.Status}\n";
                }
                
                var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
                return File(bytes, "text/csv", $"rezervasyon_raporu_{start:yyyyMMdd}_{end:yyyyMMdd}.csv");
            }
            
            return RedirectToAction("Reservations");
        }
    }
} 