using Application.Features.Brands.CQRS.Update;
using Application.Features.Brands.Queries.GetList;
using Application.Features.Brands.CQRS.Create;
using Application.Features.Brands.Queries.GetById;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Brands.CQRS.Delete;

namespace Application.Features.Brands.AutoMapper.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, CreateBrandCommand>().ReverseMap();// write
            CreateMap<Brand, CreateBrandResponse>().ReverseMap(); // read
            CreateMap<Brand, GetByIdBrandResponse>().ReverseMap(); // read
            CreateMap<Brand, DeleteBrandResponse>().ReverseMap(); // read
            CreateMap<Brand, UpdateBrandResponse>().ReverseMap(); // read
            CreateMap<Brand, GetListBrandItemDto>().ReverseMap(); // write
            CreateMap<Brand, GetByIdBrandResponse>().ReverseMap(); // read

            CreateMap<Pagnite<Brand>, GetListResponse<GetListBrandItemDto>>().ReverseMap(); // read 
        }
    }
}
