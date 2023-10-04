using FluentValidation;

namespace BookStore.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title field is required.");

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id field is required.");

            RuleFor(x => x.PageCount)
                .GreaterThan(0)
                .WithMessage("Page count must be greater than zero.");

            RuleFor(x => x.GenreId)
                .NotEmpty()
                .WithMessage("Genre Id field is required.");
        }

    }
}
