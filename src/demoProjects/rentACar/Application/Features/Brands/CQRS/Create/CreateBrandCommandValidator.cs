using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Create
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>    
    {
        public CreateBrandCommandValidator() 
        {
            RuleFor(r=> r.Name).NotEmpty().MinimumLength(2);
        }
    }
}
