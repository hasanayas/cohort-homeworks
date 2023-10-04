
// IBookService: Kitap işlemleri servisi arayüzü

// IBookService: Book operations service interface

namespace BookStore.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();
        Book GetBookById(int id);
        void AddBook(Book newBook);
        void UpdateBook(int id, Book updatedBook);
        void DeleteBook(int id);
    }
}
