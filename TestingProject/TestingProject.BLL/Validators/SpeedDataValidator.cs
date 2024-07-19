using FluentValidation;
using TestingProject.BLL.DTOs;
using TestingProject.DAL.Entities;

namespace TestingProject.BLL.Validators
{
    public class SpeedDataValidator : AbstractValidator<AddSpeedDataDTO>
    {
        public SpeedDataValidator()
        {
            RuleFor(x => x.DateTimeFormatString)
                .NotEmpty()
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
