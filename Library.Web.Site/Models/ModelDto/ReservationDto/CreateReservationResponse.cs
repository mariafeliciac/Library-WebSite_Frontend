using Library.Web.Site.Models.Model;

namespace Library.Web.Site.Models.ModelDto.ReservationDto
{
    public class CreateReservationResponse
    {
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTime EndDateReservation { get; set; }
        public bool Result { get; set; }
    }
}
