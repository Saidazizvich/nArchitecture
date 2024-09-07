using Application.Service.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetByDynamic.MediatorHandler
{
    public class GetListDynaicModelQueryHandler : IRequestHandler<GetListDynamicModelQuery, GetListResponse<GetListDynamicModelDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public async Task<GetListResponse<GetListDynamicModelDto>> Handle(GetListDynamicModelQuery request, CancellationToken cancellationToken)
        {
          Pagnite<Model> model = await _modelRepository.GetListAsyncDynamic(
                 request.DynamicQuery,
                      include: m => m.Include(b => b.Brand).Include(f => f.Fuel).Include(t => t.Trasmission),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize

                );

            var response = _mapper.Map<GetListResponse<GetListDynamicModelDto>>(model);
            return response;
        }
    }
}
