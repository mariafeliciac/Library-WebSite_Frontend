using Library.Web.Site.Models.Interfaces;
using Library.Web.Site.Models.ModelDto.BookDto;
using Library.Web.Site.Models.ShareModels;

namespace Library.Web.Site.Services
{
    public interface IBookService
    {
        public ResultAddBook AddBook(BookAddRequest book);
        public void UpdateBook(int bookid, BookEditRequest bookWithNewValues);
        public void DeleteBook(int bookid);

        public Task<IEnumerable<BookSearchResponse>> SearchBook(BookSearchRequest book);

        public List<SearchBookWithAvailabilityResponse> SearchBookWithAvailabilityInfos(BookSearchRequest book);


    }
}
