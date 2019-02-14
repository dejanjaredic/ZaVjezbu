using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RadnoMjestoVjezba.Dto;
using RadnoMjestoVjezba.Models;

namespace RadnoMjestoVjezba.MappProfils
{
    public class KancelarijaProfile : Profile
    {
        public KancelarijaProfile()
        {
            CreateMap<Kancelarija, KreiranjeKancelarijeDtocs>();
            CreateMap<KreiranjeKancelarijeDtocs, Kancelarija>();
        }
    }
}
