using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationDto>>>> GetReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(ApiResponse<IEnumerable<ReservationDto>>.SuccessResult(reservations, "Rezervasyonlar başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ReservationDto>>> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound(ApiResponse<ReservationDto>.ErrorResult("Rezervasyon bulunamadı"));
            }
            return Ok(ApiResponse<ReservationDto>.SuccessResult(reservation, "Rezervasyon başarıyla getirildi"));
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationDto>>>> GetReservationsByDate(DateTime date)
        {
            var reservations = await _reservationService.GetReservationsByDateAsync(date);
            return Ok(ApiResponse<IEnumerable<ReservationDto>>.SuccessResult(reservations, "Tarih bazlı rezervasyonlar getirildi"));
        }

        [HttpGet("table/{tableId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationDto>>>> GetReservationsByTable(int tableId)
        {
            var reservations = await _reservationService.GetReservationsByTableAsync(tableId);
            return Ok(ApiResponse<IEnumerable<ReservationDto>>.SuccessResult(reservations, "Masa bazlı rezervasyonlar getirildi"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ReservationDto>>> CreateReservation(CreateReservationDto createReservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ReservationDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdReservation = await _reservationService.CreateReservationAsync(createReservationDto);
            return CreatedAtAction(nameof(GetReservation), new { id = createdReservation.Id }, 
                ApiResponse<ReservationDto>.SuccessResult(createdReservation, "Rezervasyon başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateReservation(int id, UpdateReservationDto updateReservationDto)
        {
            if (id != updateReservationDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _reservationService.UpdateReservationAsync(updateReservationDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Rezervasyon bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Rezervasyon başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Rezervasyon bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Rezervasyon başarıyla silindi"));
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