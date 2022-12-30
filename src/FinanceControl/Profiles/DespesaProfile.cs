using AutoMapper;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Profiles
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<PostDespesaDto, DespesaModel>();
            CreateMap<DespesaModel, ReadDespesaDto>();
            CreateMap<PutDespesaDto, DespesaModel>();
            CreateMap<DespesaModel, PutDespesaDto>();
        }
    }
}
