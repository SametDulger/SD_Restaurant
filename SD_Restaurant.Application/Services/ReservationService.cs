using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(createReservationDto);
            var createdReservation = await _reservationRepository.AddAsync(reservation);
            return _mapper.Map<ReservationDto>(createdReservation);
        }

        public async Task<bool> UpdateReservationAsync(UpdateReservationDto updateReservationDto)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(updateReservationDto.Id);
            if (existingReservation == null)
                return false;

            _mapper.Map(updateReservationDto, existingReservation);
            await _reservationRepository.UpdateAsync(existingReservation);
            return true;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                return false;

            await _reservationRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByDateAsync(DateTime date)
        {
            var reservations = await _reservationRepository.GetReservationsByDateAsync(date);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByCustomerAsync(int customerId)
        {
            var reservations = await _reservationRepository.GetReservationsByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByTableAsync(int tableId)
        {
            var reservations = await _reservationRepository.GetReservationsByTableAsync(tableId);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync()
        {
            var reservations = await _reservationRepository.GetActiveReservationsAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
    }
} 