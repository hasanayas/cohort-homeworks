using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                //Example Genre Datas
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Fantasy"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    }
                );

                //Example Aythor Datas
                context.Authors.AddRange(
                   new Author
                   {
                       FirstName = "J.K.",
                       LastName = "Rowling",
                       DateOfBirth = new DateTime(1965, 07, 31)
                   },
                    new Author
                    {
                        FirstName = "J.R.R.",
                        LastName = "Tolkien",
                        DateOfBirth = new DateTime(1892, 01, 03)
                    },
                    new Author
                    {
                        FirstName = "Philip",
                        LastName = "K. Dick",
                        DateOfBirth = new DateTime(1928, 12, 16)
                    }
                );

                //Example Book Datas
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
                context.SaveChanges();
            }
        }
    }
}