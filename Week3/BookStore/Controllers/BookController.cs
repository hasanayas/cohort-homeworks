using BookStore.Attributes;
using BookStore.Data;
using BookStore.DbOperations;
using BookStore.Validators;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.CreateBook;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.UpdateBook;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private static List<Book> BookList = BookData.BookList;


        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


        //GET
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBookQuery query = new GetBookQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }


        //GETBYID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context);
            query.BookId = id;
            result = query.Handle();
            return Ok(result);
        }


        //CREATE
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);

            try
            {
                command.Model = newBook;
                command.Handle();

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
               
        }


        //UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            command.Handle();
            return Ok();
        }


        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            command.Handle();
            return Ok();
        }


        [HttpGet("list")]
        public List<Book> GetBooksByName([FromQuery] string name)
        {
            var bookList = BookList.Where(x => x.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return bookList;
        }

        //Yetkisiz Giriş 
      
        [HttpGet("FakeUser")]
        [AuthorizeFakeUser]
        public List<Book> GetBooksForFakeUser([FromQuery] string name)
        {
            var bookList = BookList.Where(x => x.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return bookList;
        }
    }
}
