using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Interfaces
{
    public interface IReceitaRepository
    {
        ReceitaModel AddReceita(PostReceitaDto postReceitaDto);
        void DeleteReceita(int id);
        List<ReadReceitaDto> GetReceitaByDate(int ano, int mes);
        List<ReadReceitaDto> GetReceitaByDescricao(string descricao);
        ReadReceitaDto GetReceitaById(int id);
        List<ReadReceitaDto> GetReceitas();
        void PutReceita(int id, PutReceitaDto putReceitaDto);
    }
}