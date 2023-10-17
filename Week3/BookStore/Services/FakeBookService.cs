
// FakeBookService: Sahte kitap servisi, IBookService arayüzünü uygular. 

// FakeBookService: Implements the IBookService interface for a fake book service.


namespace BookStore.Services
{
    public class FakeBookService : IBookService
    {
        private static List<Book> BookList = Data.BookData.BookList;

        public List<Book> GetBooks()
        {
            return BookList.OrderBy(x => x.Id).ToList();
        }

        public Book GetBookById(int id)
        {
            return BookList.SingleOrDefault(x => x.Id == id);
        }

        public void AddBook(Book newBook)
        {
            BookList.Add(newBook);
        }

        public void UpdateBook(int id, Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book != null)
            {
                book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
                book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
                book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
                book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            }
        }

        public void DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book != null)
            {
                BookList.Remove(book);
            }
        }
    }
}
