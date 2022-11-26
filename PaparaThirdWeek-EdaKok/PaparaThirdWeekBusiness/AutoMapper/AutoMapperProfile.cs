using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaThirdWeek.Services.DTOS;
using PaparaThirdWeek.Domain.Entities;

namespace PaparaThirdWeek.Business.AutoMapper
{
   public   class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<FakeDataUser, FakeDataUserDTO>().ReverseMap();

        }
    }
}
