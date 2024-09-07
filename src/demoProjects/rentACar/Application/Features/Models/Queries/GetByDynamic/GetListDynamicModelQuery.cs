using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistance.Dynamic;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetByDynamic
{
    public class GetListDynamicModelQuery  : IRequest<GetListResponse<GetListDynamicModelDto>>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }
    }
}
