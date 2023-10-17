using BookStore.DbOperations;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        //Kitap Id'ye güncelleme yapar
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }


        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {

            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book is null)
            {
                throw new InvalidOperationException("Book doesn't exist.");
            }

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
