using MediatR;
using FluentValidation;
using App.Request.Command;

namespace App.Validtion.Command
{
    public class AddProductCommandValidtion : AbstractValidator<AddProductCommand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}