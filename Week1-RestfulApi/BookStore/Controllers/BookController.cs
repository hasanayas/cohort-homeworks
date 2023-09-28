using BookStore.Validators;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = Data.BookData.BookList;

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var validator = new BookValidator();
            var result = validator.Validate(newBook);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var bookWithSameTitle = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            var bookWithSameId = BookList.SingleOrDefault(x => x.Id == newBook.Id);

            if (bookWithSameTitle != null)
                return BadRequest("There is already a book with this title.");

            if (bookWithSameId != null)
                return BadRequest("There is already a book with this ID number.");

            BookList.Add(newBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound("Book not found.");

            var validator = new BookValidator();
            var result = validator.Validate(updatedBook);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound("Book not found.");

            BookList.Remove(book);
            return Ok();
        }

        [HttpGet("list")]
        public List<Book> GetBooksByName([FromQuery] string name)
        {
            var bookList = BookList.Where(x => x.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return bookList;
        }

    }
}