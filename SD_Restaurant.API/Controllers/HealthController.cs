using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        /// <summary>
        /// Sistem sağlık durumunu kontrol eder
        /// </summary>
        /// <returns>Sistem sağlık durumu</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<object>>> GetHealth()
        {
            var healthReport = await _healthCheckService.CheckHealthAsync();

            var status = healthReport.Status == HealthStatus.Healthy ? "Healthy" : "Unhealthy";
            var message = healthReport.Status == HealthStatus.Healthy 
                ? "Sistem sağlıklı çalışıyor" 
                : "Sistemde sorunlar var";

            return Ok(ApiResponse<object>.SuccessResult(new
            {
                Status = status,
                Timestamp = DateTime.UtcNow,
                Checks = healthReport.Entries.Select(e => new
                {
                    Name = e.Key,
                    Status = e.Value.Status.ToString(),
                    Description = e.Value.Description
                })
            }, message));
        }

        /// <summary>
        /// Veritabanı bağlantısını kontrol eder
        /// </summary>
        /// <returns>Veritabanı durumu</returns>
        [HttpGet("database")]
        public async Task<ActionResult<ApiResponse<object>>> GetDatabaseHealth()
        {
            var healthReport = await _healthCheckService.CheckHealthAsync(reg => reg.Tags.Contains("database"));

            var status = healthReport.Status == HealthStatus.Healthy ? "Connected" : "Disconnected";
            var message = healthReport.Status == HealthStatus.Healthy 
                ? "Veritabanı bağlantısı başarılı" 
                : "Veritabanı bağlantısında sorun var";

            return Ok(ApiResponse<object>.SuccessResult(new
            {
                Status = status,
                Timestamp = DateTime.UtcNow
            }, message));
        }
    }
} 