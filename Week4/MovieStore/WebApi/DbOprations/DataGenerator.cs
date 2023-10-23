using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOprations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }

                context.Movies.AddRange(

                    new Movie
                    {
                        // ID = 1,
                        GenreID = 1,
                        Title = "Harry Potter ve Felsefe Taşı",
                        Year = "2002",
                        Director = "Chris Columbus",
                        Actors = "Daniel Radcliffe, Emma Watson, Rupert Grint ",
                        Price = 100,
                        IsActive = true

                    },

                    new Movie
                    {
                        // ID = 2,
                        GenreID = 3,
                        Title = "Tenet",
                        Year = "2022",
                        Director = "Christopher Nolan",
                        Actors = " Elizabeth Debicki, John Washington Robert Pattinson",
                        Price = 100,
                        IsActive = true

                    }
                    );

                context.Directors.AddRange(
                 new Director { Name = "Chris", LastName = "Columbu", FilmsDirected = "Harry Potter ve Felsefe Taşı", IsActive = true },
                 new Director { Name = "Christopher", LastName = "Nolan", FilmsDirected = "Harry Potter ve Felsefe Taşı", IsActive = true }
                  );

                context.SaveChanges();

                context.Actors.AddRange(
                  new Actor { Name = "Daniel", LastName = "Radcliffe", PlayedMovies = "Harry Potter ve Felsefe Taşı", IsAvtive = true },
                  new Actor { Name = "Emma", LastName = "Watson", PlayedMovies = "Harry Potter ve Felsefe Taşı", IsAvtive = true },
                  new Actor { Name = "Rupert", LastName = "Grint", PlayedMovies = "Harry Potter ve Felsefe Taşı", IsAvtive = true },

                  new Actor { Name = "Elizabeth", LastName = "Debicki", PlayedMovies = "Tenet", IsAvtive = true },
                  new Actor { Name = "Robert", LastName = "Pattinson", PlayedMovies = "Tenet", IsAvtive = true },
                  new Actor { Name = "John", LastName = "Washington", PlayedMovies = "Tenet", IsAvtive = true }
                  );
                context.SaveChanges();

                

                context.Genres.AddRange(
                      new Genre
                      {
                          Name = "Bilimkurgu "
                      },


                       new Genre
                       {
                           Name = "Fantastik "
                       }
            
               );
                context.SaveChanges();

                context.Customers.AddRange(

                 new Customer
                 {
                     Name = "Test",
                     LastName = "Test",
                     Email = "test@mail.com",
                     Password = "12345",
                     IsActive = true

                 },


                 new Customer
                 {
                     Name = "Test1",
                     LastName = "Test",
                     Email = "test1@mail.com",
                     Password = "12345",
                     IsActive = true

                 }
             );


                context.SaveChanges();

                context.Orders.AddRange(
                  new Order { CustomerId = 1 , MovieId = 1, purchasedTime = new DateTime(2023, 01, 01) , IsActive = true },
                  new Order { CustomerId = 2 , MovieId = 1, purchasedTime = new DateTime(2023, 01, 01) , IsActive = true }
                  );

                context.SaveChanges();

            }
        }

    }
}
