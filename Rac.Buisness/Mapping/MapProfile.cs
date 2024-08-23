using AutoMapper;
using Rac.entity;
using Rac.entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.Buisness.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
           CreateMap<Car, CarDto>().ReverseMap();
           CreateMap<Customer, CustomerDto>().ReverseMap();
           CreateMap<Reservation, ReservationDto>().ReverseMap();
            
        }
    }
}
