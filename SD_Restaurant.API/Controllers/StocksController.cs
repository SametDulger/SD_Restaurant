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
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStocks()
        {
            var stocks = await _stockService.GetAllStocksAsync();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetStock(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStocksByLocation(string location)
        {
            var stocks = await _stockService.GetStocksByLocationAsync(location);
            return Ok(stocks);
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStocksByProduct(int productId)
        {
            var stocks = await _stockService.GetStocksByProductAsync(productId);
            return Ok(stocks);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetLowStockItems()
        {
            var stocks = await _stockService.GetLowStockItemsAsync();
            return Ok(stocks);
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
        public async Task<ActionResult<StockDto>> CreateStock(CreateStockDto createStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStock = await _stockService.CreateStockAsync(createStockDto);
            return CreatedAtAction(nameof(GetStock), new { id = createdStock.Id }, createdStock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, UpdateStockDto updateStockDto)
        {
            if (id != updateStockDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _stockService.UpdateStockAsync(updateStockDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            await _stockService.DeleteStockAsync(id);
            return NoContent();
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
    }
} 