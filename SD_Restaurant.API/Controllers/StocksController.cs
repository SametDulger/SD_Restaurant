using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.Services;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StockDto>>>> GetStocks()
        {
            var stocks = await _stockService.GetAllStocksAsync();
            return Ok(ApiResponse<IEnumerable<StockDto>>.SuccessResult(stocks, "Stoklar başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StockDto>>> GetStock(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound(ApiResponse<StockDto>.ErrorResult("Stok bulunamadı"));
            }
            return Ok(ApiResponse<StockDto>.SuccessResult(stock, "Stok başarıyla getirildi"));
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StockDto>>>> GetStocksByLocation(string location)
        {
            var stocks = await _stockService.GetStocksByLocationAsync(location);
            return Ok(ApiResponse<IEnumerable<StockDto>>.SuccessResult(stocks, "Konum bazlı stoklar getirildi"));
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StockDto>>>> GetStocksByProduct(int productId)
        {
            var stocks = await _stockService.GetStocksByProductAsync(productId);
            return Ok(ApiResponse<IEnumerable<StockDto>>.SuccessResult(stocks, "Ürün bazlı stoklar getirildi"));
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StockDto>>>> GetLowStockItems()
        {
            var stocks = await _stockService.GetLowStockItemsAsync();
            return Ok(ApiResponse<IEnumerable<StockDto>>.SuccessResult(stocks, "Düşük stok ürünleri getirildi"));
        }

        [HttpGet("check-availability")]
        public async Task<ActionResult<bool>> CheckStockAvailability(
            [FromQuery] int productId, 
            [FromQuery] string location, 
            [FromQuery] decimal quantity)
        {
            var isAvailable = await _stockService.CheckStockAvailabilityAsync(productId, location, quantity);
            return Ok(isAvailable);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<StockDto>>> CreateStock(CreateStockDto createStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<StockDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdStock = await _stockService.CreateStockAsync(createStockDto);
            return CreatedAtAction(nameof(GetStock), new { id = createdStock.Id }, 
                ApiResponse<StockDto>.SuccessResult(createdStock, "Stok başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateStock(int id, UpdateStockDto updateStockDto)
        {
            if (id != updateStockDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _stockService.UpdateStockAsync(updateStockDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Stok bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Stok başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteStock(int id)
        {
            var result = await _stockService.DeleteStockAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Stok bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Stok başarıyla silindi"));
        }

        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateStockQuantity(
            [FromQuery] int productId, 
            [FromQuery] string location, 
            [FromQuery] decimal quantity)
        {
            await _stockService.UpdateStockQuantityAsync(productId, location, quantity);
            return NoContent();
        }

        private List<string> GetModelStateErrors()
        {
            var errors = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
} 