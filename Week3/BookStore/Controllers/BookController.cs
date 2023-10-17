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
using FluentValidation;
using System.Reflection.Metadata;
using BookStore.BookOperations.GetBookDetail;

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
            var result = query.Handle();  // Call Handle function
            return Ok(result);
        }


        //GET BY ID
        [HttpGet("{id}")] // Call Handle function
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query); //Validator
                result = query.Handle(); // Call Handle function
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command); //Validator
                command.Handle(); // Call Handle function

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
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command); //Validator
                command.Handle(); // Call Handle function
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;

                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command); //Validator
                command.Handle(); // Call Handle function
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
