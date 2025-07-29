using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;
using SD_Restaurant.Core.Enums;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<TableDto>>>> GetTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(ApiResponse<IEnumerable<TableDto>>.SuccessResult(tables, "Masalar başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TableDto>>> GetTable(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound(ApiResponse<TableDto>.ErrorResult("Masa bulunamadı"));
            }
            return Ok(ApiResponse<TableDto>.SuccessResult(table, "Masa başarıyla getirildi"));
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TableDto>>>> GetTablesByStatus(string status)
        {
            if (Enum.TryParse<TableStatus>(status, true, out var tableStatus))
            {
                var tables = await _tableService.GetTablesByStatusAsync(tableStatus);
                return Ok(ApiResponse<IEnumerable<TableDto>>.SuccessResult(tables, "Durum bazlı masalar getirildi"));
            }
            return BadRequest(ApiResponse<IEnumerable<TableDto>>.ErrorResult("Geçersiz durum değeri"));
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TableDto>>>> GetTablesByLocation(string location)
        {
            var tables = await _tableService.GetTablesByLocationAsync(location);
            return Ok(ApiResponse<IEnumerable<TableDto>>.SuccessResult(tables, "Konum bazlı masalar getirildi"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TableDto>>> CreateTable(CreateTableDto createTableDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<TableDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdTable = await _tableService.CreateTableAsync(createTableDto);
            return CreatedAtAction(nameof(GetTable), new { id = createdTable.Id }, 
                ApiResponse<TableDto>.SuccessResult(createdTable, "Masa başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateTable(int id, UpdateTableDto updateTableDto)
        {
            if (id != updateTableDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _tableService.UpdateTableAsync(updateTableDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Masa bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Masa başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Masa bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Masa başarıyla silindi"));
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