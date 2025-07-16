using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date);
        Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId);
        Task<IEnumerable<Reservation>> GetReservationsByTableAsync(int tableId);
        Task<IEnumerable<Reservation>> GetActiveReservationsAsync();
        Task<bool> IsTableAvailableForReservationAsync(int tableId, DateTime date, int numberOfGuests);
    }
} 