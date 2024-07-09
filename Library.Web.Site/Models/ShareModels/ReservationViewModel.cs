using Library.Web.Site.Models.Interfaces;

namespace Library.Web.Site.Models.ShareModels
{
    public class ReservationViewModel
    {
        public IBook Book { get; set; }

        public IUser User { get; set; }

        public IReservation Reservation { get; set; }
        public ReservationStatus ReservationStatus {  get; set; }
    }
}
