using FluentValidation;
using TestingProject.BLL.DTOs;
using TestingProject.DAL.Errors;


namespace TestingProject.BLL.Validators
{
    public class SpeedDataValidator : AbstractValidator<AddSpeedDataDTO>
    {
        public SpeedDataValidator()
        {
            RuleFor(x => x.DateTimeFormatString)
                .NotEmpty()
                .WithMessage(ErrorConsts.DateOrTimeError);

            RuleFor(x => x.CarNumber)
                .NotEmpty()
                .MaximumLength(12)
                .WithMessage(ErrorConsts.CarNumberError);

            RuleFor(x => x.Speed)
                .NotEmpty()
                .ExclusiveBetween(0.0, 500.0)
                .WithMessage(ErrorConsts.SpeedError);
        }
    }
}
