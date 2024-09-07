using Application.Service.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Update.MediatorHandler
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdateBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public UpdateBrandCommandHandler(IMapper imapper, IBrandRepository brandRepository)
        {
            _mapper = imapper;
            _brandRepository = brandRepository;
        }

        public async Task<UpdateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(perdicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            brand = _mapper.Map<Brand>(request);
            await _brandRepository.UpdateAsync(brand);

            UpdateBrandResponse response = _mapper.Map<UpdateBrandResponse>(brand);
            return response;


        }
    }
}
