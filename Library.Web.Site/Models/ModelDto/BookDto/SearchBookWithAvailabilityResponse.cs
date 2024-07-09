using Library.Web.Site.Models.ShareModels;

namespace Library.Web.Site.Models.ModelDto.BookDto
{
    public class SearchBookWithAvailabilityResponse : BookSearchResponse
    {

        public AvailabilityBook AvailabilityBook { get; set; }

        public DateTime AvailabilityDate {  get; set; }




    }
}
