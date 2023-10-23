using FluentValidation;
using WebApi.Application.ActorOperations.Commands.CreateActor;

namespace WebApi.Application.ActorOperations.Validator
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(a => a.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(a => a.Model.LastName).NotEmpty().MinimumLength(4);
            RuleFor(a => a.Model.PlayedMovies).NotEmpty().MinimumLength(4);
        }
    }
}
