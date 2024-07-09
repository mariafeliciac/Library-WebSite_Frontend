using Library.Web.Site.Models.Interfaces;

namespace Library.Web.Site.Models.ShareModels
{
    public class BookViewModel
    {
        public IBook Book { get; set; }
        public List<ReservationViewModel> ReservationsViewModel { get; set; }

        public AvailabilityBook AvailabilityBook {  get; set; }
        public DateTime AvailabilityDate {  get; set; }


    }
}
