using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Update
{
    public class UpdateBrandCommand : IRequest<UpdateBrandResponse>
    {
        public Guid Id { get; set; }
    }
}
