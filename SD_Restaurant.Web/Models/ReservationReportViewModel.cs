using System.Collections.Generic;

namespace SD_Restaurant.Web.Models
{
    public class ReservationReportViewModel
    {
        public List<ReservationViewModel> Reservations { get; set; } = new List<ReservationViewModel>();
        public int TotalReservations { get; set; }
        public int TodayReservations { get; set; }
        public int ThisWeekReservations { get; set; }
        public int ActiveReservations => Reservations.Count(r => r.Status == "Active");
        public int CompletedReservations => Reservations.Count(r => r.Status == "Completed");
        public int CancelledReservations => Reservations.Count(r => r.Status == "Cancelled");
    }
} 