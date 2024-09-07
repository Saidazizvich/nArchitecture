using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Delete
{
    public class DeleteBrandCommand : IRequest<DeleteBrandResponse>
    {
        public Guid Id { get; set; }
    }
}
