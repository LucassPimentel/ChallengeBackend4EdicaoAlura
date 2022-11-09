using AutoMapper;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Profiles
{
    public class ReceitasProfle : Profile
    {
        public ReceitasProfle()
        {
            CreateMap<PostReceitaDto, Receita>();
            CreateMap<Receita, ReadReceitaDto>();
            CreateMap<ReadReceitaDto, PutReceitaDto>().ReverseMap();
            CreateMap<PutReceitaDto, Receita>();
        }
    }
}
