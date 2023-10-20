using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "Harry Potter and the Philosopher's Stone",
                    PageCount = 320,
                    PublishDate = new DateTime(1997, 06, 26),
                    GenreId = 1, // Fantasy
                    AuthorId = 1 // J.K. Rowling
                },
                    new Book
                    {
                        Title = "Harry Potter and the Chamber of Secrets",
                        PageCount = 352,
                        PublishDate = new DateTime(1998, 07, 02),
                        GenreId = 1, // Fantasy
                        AuthorId = 1 // J.K. Rowling
                    },
                    new Book
                    {
                        Title = "The Fellowship of the Ring",
                        PageCount = 423,
                        PublishDate = new DateTime(1954, 07, 29),
                        GenreId = 1, // Fantasy
                        AuthorId = 2 // J.R.R. Tolkien
                    },
                    new Book
                    {
                        Title = "The Two Towers",
                        PageCount = 352,
                        PublishDate = new DateTime(1954, 11, 11),
                        GenreId = 1, // Fantasy
                        AuthorId = 2 // J.R.R. Tolkien
                    },
                    new Book
                    {
                        Title = "Dune",
                        PageCount = 540,
                        PublishDate = new DateTime(1965, 08, 21),
                        GenreId = 2, // Science Fiction
                        AuthorId = 3 // Philip K. Dick
                    }
            );
        }
    }
}