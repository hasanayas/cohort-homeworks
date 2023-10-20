using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}