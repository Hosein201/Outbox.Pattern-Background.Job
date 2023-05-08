using MediatR;
using FluentValidation;
using App.Request.Command;

namespace App.Validtion.Command
{
    public class AddProductCommandValidtion : AbstractValidator<AddProductCommand>
    {

        public AddProductCommandValidtion()
        {
            RuleFor(command=> command.Name)
                .NotEmpty()
                .NotNull()
                .WithName("NameIsNull"); 
            
            RuleFor(command=> command.Description)
                .NotEmpty()
                .NotNull()
                .WithName("DescriptionIsNull");
        }
    }
}