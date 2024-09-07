using Application.Features.Brands.CQRS.Create;
using Application.Features.Rules;
using Application.Service.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Create.MediatorHandler
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreateBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository,BrandBusinessRules brandBusinessRules)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreateBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCannotBeDuplecatedWhenInserted(request.Name); // hata yontemi

            var result = _mapper.Map<Brand>(request);

            var response = await _brandRepository.AddAsync(result);

            CreateBrandResponse createBrandResponse = _mapper.Map<CreateBrandResponse>(response);
            createBrandResponse.Name = request.Name;
            createBrandResponse.Id = new Guid();
            return null;

        }
    }
}
