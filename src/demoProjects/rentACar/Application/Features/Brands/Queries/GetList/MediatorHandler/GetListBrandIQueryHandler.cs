using Application.Features.Brands.Queries.GetList;
using Application.Service.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetList.MediatorHandler
{
    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandItemQuery, GetListResponse<GetListBrandItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public GetListBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<GetListResponse<GetListBrandItemDto>> Handle(GetListBrandItemQuery request, CancellationToken cancellationToken)
        {
            Pagnite<Brand> pagnite = await _brandRepository.GetListAsync(
                index: request.PageRequest.PageIndex, // sayfa 1 2  3 4 
                 size: request.PageRequest.PageSize, // boyut veri ler
                 cancellationToken: cancellationToken
            );

            GetListResponse<GetListBrandItemDto> getListResponse = _mapper.Map<GetListResponse<GetListBrandItemDto>>(pagnite);

            return getListResponse;
        }
    }
}
