using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOprations
{
    public interface IMovieStoreDbContext
    {

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }

        int SaveChanges();
    }
}
