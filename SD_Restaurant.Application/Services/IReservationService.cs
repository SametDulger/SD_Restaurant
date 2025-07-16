using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task<ReservationDto> CreateReservationAsync(CreateReservationDto createReservationDto);
        Task<bool> UpdateReservationAsync(UpdateReservationDto updateReservationDto);
        Task<bool> DeleteReservationAsync(int id);
        Task<IEnumerable<ReservationDto>> GetReservationsByDateAsync(DateTime date);
        Task<IEnumerable<ReservationDto>> GetReservationsByCustomerAsync(int customerId);
        Task<IEnumerable<ReservationDto>> GetReservationsByTableAsync(int tableId);
        Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync();
    }
} 