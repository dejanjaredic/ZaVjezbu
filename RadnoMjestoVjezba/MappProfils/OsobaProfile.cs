using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RadnoMjestoVjezba.Dto;
using RadnoMjestoVjezba.Models;

namespace RadnoMjestoVjezba.MappProfils
{
    public class OsobaProfile : Profile
    {
        public OsobaProfile()
        {
            CreateMap<Osoba, OsobaDto>();
            CreateMap<OsobaDto, Osoba>();
        }
    }
}
