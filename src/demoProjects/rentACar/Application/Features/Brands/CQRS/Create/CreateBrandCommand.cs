using Core.Application.Pipellines.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Create
{
    public class CreateBrandCommand : IRequest<CreateBrandResponse>,ITransactionalRequest
    {
        public string Name { get; set; }
    }
}
