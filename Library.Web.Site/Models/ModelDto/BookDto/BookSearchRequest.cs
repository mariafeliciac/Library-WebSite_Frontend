using System.Runtime.InteropServices;

namespace Library.Web.Site.Models.ModelDto.BookDto
{
    public class BookSearchRequest
    {
        public string? Title { get; set; }

        public string? AuthorName { get; set; }

        public string? AuthorSurname { get; set; }

        public string? PublishingHouse { get; set; }
    }
}
