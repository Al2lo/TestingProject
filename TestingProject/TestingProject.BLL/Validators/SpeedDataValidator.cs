using FluentValidation;
using TestingProject.DAL.Entities;

namespace TestingProject.BLL.Validators
{
    public class SpeedDataValidator : AbstractValidator<SpeedData>
    {
        public SpeedDataValidator()
        {
            RuleFor(x => x.DateTime)
                .NotEmpty()
                .GreaterThanOrEqualTo(new DateTime(2000, 1, 1))
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Incorrect date or time!");

            RuleFor(x => x.CarNumber)
                .NotEmpty()
                .MaximumLength(12)
                .WithMessage("Incorrect auto number!");

            RuleFor(x => x.Speed)
                .NotEmpty()
                .ExclusiveBetween(0.0, 500.0)
                .WithMessage("Incorrect auto Speed!");
        }
    }
}
