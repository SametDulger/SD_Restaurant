using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableDto>> GetTable(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTablesByStatus(string status)
        {
            var tables = await _tableService.GetTablesByStatusAsync(status);
            return Ok(tables);
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTablesByLocation(string location)
        {
            var tables = await _tableService.GetTablesByLocationAsync(location);
            return Ok(tables);
        }

        [HttpPost]
        public async Task<ActionResult<TableDto>> CreateTable(CreateTableDto createTableDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTable = await _tableService.CreateTableAsync(createTableDto);
            return CreatedAtAction(nameof(GetTable), new { id = createdTable.Id }, createdTable);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, UpdateTableDto updateTableDto)
        {
            if (id != updateTableDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _tableService.UpdateTableAsync(updateTableDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTableStatus(int id, [FromBody] string status)
        {
            var result = await _tableService.UpdateTableStatusAsync(id, status);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
} 