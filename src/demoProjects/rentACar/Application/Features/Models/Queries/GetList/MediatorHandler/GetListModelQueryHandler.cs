using Application.Service.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetList.MediatorHandler
{
    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository; 

        public async Task<GetListResponse<GetListModelListItemDto>> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
          Pagnite<Model> model = await _modelRepository.GetListAsync(
                  include: m=> m.Include(b=>b.Brand).Include(f=>f.Fuel).Include(t=>t.Trasmission),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize

          );
            
               var response = _mapper.Map<GetListResponse<GetListModelListItemDto>>(model);
            return response;
        }
    }
}
