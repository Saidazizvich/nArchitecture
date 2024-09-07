using Application.Features.Models.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = Domain.Entity.Model;

namespace Application.Features.Models.AutoMapper.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Model, GetListModelListItemDto>()
                .ForMember(destinationMember: m => m.BrandName, memberOptions: b => b.MapFrom(b => b.Brand.Name))
                .ForMember(destinationMember: f => f.FuelName, memberOptions: f => f.MapFrom(f => f.Fuel.Name))
                .ForMember(destinationMember: t => t.TranmissionName, memberOptions: t => t.MapFrom(t => t.Trasmission.Name))
                
                .ReverseMap();
             CreateMap<Pagnite<Model>,GetListResponse<GetListModelListItemDto>>().ReverseMap();
        }
    }
}
