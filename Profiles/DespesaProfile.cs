using AutoMapper;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Profiles
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<PostDespesaDto, Despesa>();
            CreateMap<Despesa, ReadDespesaDto>();
            CreateMap<PutDespesaDto, Despesa>();
        }
    }
}
