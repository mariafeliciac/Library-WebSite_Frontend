using Library.Web.Site.Models.Interfaces;
using Library.Web.Site.Models.Model;
using Library.Web.Site.Models.ModelDto.BookDto;
using Library.Web.Site.Models.ShareModels;
using Library.Web.Site.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Site.Controllers
{

    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) 
        {
            _bookService = bookService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(BookSearchRequest bookSearchRequest)
        {
            var allBooks = await _bookService.SearchBook(bookSearchRequest);
            return View(allBooks);
        }



        [HttpGet]
        public IActionResult CreateForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookAddRequest book)
        {
           var resultAddBook = _bookService.AddBook(book);
            return View(resultAddBook);
        }

        [HttpGet]
        public IActionResult Details(BookSearchRequest book)
        {
            var bookWithAvailable = _bookService.SearchBookWithAvailabilityInfos(book);
            return View(bookWithAvailable.SingleOrDefault());
        }

        [HttpGet]
        public IActionResult EditForm(Book book)
        {
           return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            string message;
            try
            {
                _bookService.UpdateBook(book.BookId, new BookEditRequest { Title = book.Title, AuthorName = book.AuthorName, AuthorSurname = book.AuthorSurname, PublishingHouse = book.PublishingHouse });
                message = "\r\nBook edited successfully!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
          return View((object)message);    
        }

        [HttpGet]
        public IActionResult DeleteBookInformation(Book book)
        {
            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            List<string> messages = new List<string>();
            try
            {
                _bookService.DeleteBook(book.BookId);
               messages.Add($"The book {book.Title} has been deleted!");
            }
            catch (AggregateException ex)
            {
                ex.InnerExceptions.ToList().ForEach(e => messages.Add(e.Message));
            }
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> IndexPost([FromBody] BookSearchRequest book)
        {
            var books = await _bookService.SearchBook(book);
            return PartialView("_SearchBook", books);
        }


    }
}
