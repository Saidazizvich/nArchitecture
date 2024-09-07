using Application.Features.Brands.CQRS.Delete;
using Application.Features.Brands.CQRS.Update;
using Application.Service.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Delete.MediatorHandler
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public DeleteBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<DeleteBrandResponse>? Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(perdicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            brand = _mapper.Map<Brand>(request);
            await _brandRepository.DeleteAsync(brand);

            //UpdateBrandResponse? brandResponse = _mapper.Map<UpdateBrandResponse>(brand); 111

            DeleteBrandResponse response = _mapper.Map<DeleteBrandResponse>(brand); // 222

            return response;
        }
    }
}
