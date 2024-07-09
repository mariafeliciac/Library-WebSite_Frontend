using Library.Web.Site.Models.ShareModels;

namespace Library.Web.Site.Models.ModelDto.ReservationDto
{
    public class GetReservationRequest
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public ReservationStatus? ReservationStatus { get; set; }
    }
}
