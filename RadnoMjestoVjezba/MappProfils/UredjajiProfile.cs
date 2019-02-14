using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RadnoMjestoVjezba.Dto;
using RadnoMjestoVjezba.Models;

namespace RadnoMjestoVjezba.MappProfils
{
    public class UredjajiProfile : Profile
    {
        public UredjajiProfile()
        {
            CreateMap<Uredjaj, UredjajDto>();
            CreateMap<UredjajDto, Uredjaj>();
        }
    }
}
