using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetList
{
    public class GetListBrandItemQuery : IRequest<GetListResponse<GetListBrandItemDto>>
    {
        public PageRequest PageRequest { get; set; }
    }
}
