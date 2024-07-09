using Library.Web.Site.Models.Interfaces;
using Library.Web.Site.Models.Model;
using Library.Web.Site.Models.ModelDto.BookDto;
using Library.Web.Site.Models.ShareModels;

namespace Library.Web.Site.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;
        public BookService(IHttpClientFactory httpClientFactory, IConfigurationRoot configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["Uri"]);
        }


        public ResultAddBook AddBook(BookAddRequest book)
        {
            HttpResponseMessage response = _httpClient.PostAsJsonAsync("api/Book", book).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ReadFromJsonAsync<ResultAddBook>().Result;
            }

            return 0;
        }

        public void DeleteBook(int bookid)
        {
            string queryString = $"?bookId={bookid}";
            var response = _httpClient.DeleteAsync($"api/Book{queryString}").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var messages = response.Content.ReadFromJsonAsync<List<string>>().Result;

                List<Exception> exceptions = new List<Exception>();

                if (messages != null && messages.Count > 0)
                {
                    foreach (var message in messages)
                    {
                        exceptions.Add(new Exception(message));
                    }
                }

                throw new AggregateException(exceptions);
            }
        }

        public async Task<IEnumerable<BookSearchResponse>> SearchBook(BookSearchRequest book)
        {
            string queryString = $"?title={book.Title}&authorname={book.AuthorName}&authorsurname={book.AuthorSurname}&publishingHouse={book.PublishingHouse}";

            var response = _httpClient.GetAsync($"api/Book{queryString}").Result;

            var listBooks = new List<BookSearchResponse>();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.Content.ReadFromJsonAsync<List<BookSearchResponse>>().Result.ForEach(r => listBooks.Add(new BookSearchResponse
                {
                    BookId = r.BookId,
                    Title = r.Title,
                    AuthorName = r.AuthorName,
                    AuthorSurname = r.AuthorSurname,
                    PublishingHouse = r.PublishingHouse,
                    Quantity = r.Quantity
                }));
            }

            return listBooks;
        }



        public List<SearchBookWithAvailabilityResponse> SearchBookWithAvailabilityInfos(BookSearchRequest book)
        {
            string queryString = $"?title={book.Title}&authorname={book.AuthorName}&authorsurname={book.AuthorSurname}&publishingHouse={book.PublishingHouse}";

            var response = _httpClient.GetAsync($"api/Book/BooksWithAvailability{queryString}").Result;

            var listBooks = new List<SearchBookWithAvailabilityResponse>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.Content.ReadFromJsonAsync<List<SearchBookWithAvailabilityResponse>>().Result.ForEach(r => listBooks.Add(new SearchBookWithAvailabilityResponse
                {
                        BookId = r.BookId,
                        Title = r.Title,
                        AuthorName = r.AuthorName,
                        AuthorSurname = r.AuthorSurname,
                        PublishingHouse = r.PublishingHouse,
                        Quantity = r.Quantity,
                    AvailabilityBook = r.AvailabilityBook,
                    AvailabilityDate = r.AvailabilityDate
                }));
                return listBooks;
            }
            return listBooks;
        }


        public void UpdateBook(int bookid, BookEditRequest bookWithNewValues)
        {
            string queryString = $"?lastBookId={bookid}";

            var bookAddRequest = new BookEditRequest
            {
                Title = bookWithNewValues.Title,
                AuthorName = bookWithNewValues.AuthorName,
                AuthorSurname = bookWithNewValues.AuthorSurname,
                PublishingHouse = bookWithNewValues.PublishingHouse
            };
            var response = _httpClient.PutAsJsonAsync<BookEditRequest>($"api/Book{queryString}", bookAddRequest).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var message = response.Content.ReadAsStringAsync().Result;
                throw new Exception(message);
            }

        }

    }
}
