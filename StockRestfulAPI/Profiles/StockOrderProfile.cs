using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockRestfulAPI;
using StockRestfulAPI.Model;

namespace StockRestfulAPI.Profiles
{
    public class StockOrderProfile : Profile
    {
        public StockOrderProfile()
        {
            CreateMap<StockOrder, StockOrderDto>()
                //Projection
                .ForMember(
                    dest => dest.StartDate, //destination
                    opt => opt.MapFrom(src => DateTime.Parse(src.StartDate)) //how we transform the data from source to destination.
                ).ForMember(
                    dest => dest.EndDate, //destination
                    opt => opt.MapFrom(src => DateTime.Parse(src.EndDate)) //how we transform the data from source to destination.
                );
        }
    }
}
