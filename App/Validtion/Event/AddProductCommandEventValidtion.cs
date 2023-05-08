using App.Request.Event;
using FluentValidation;

namespace App.Validtion.Event
{
    public class AddProductCommandEventValidtion : AbstractValidator<AddProductCommandEvent>
    {
        public AddProductCommandEventValidtion()
        {
            RuleFor(command => command.Name)
               .NotEmpty()
               .NotNull()
               .WithName("NameIsNull");

            RuleFor(command => command.Description)
                .NotEmpty()
                .NotNull()
                .WithName("DescriptionIsNull");
        }
    }
}