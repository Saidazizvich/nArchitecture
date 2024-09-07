using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetList
{
    public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
    }
}
