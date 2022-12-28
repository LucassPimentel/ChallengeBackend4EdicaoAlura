using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Models;

namespace ChallengeBackend4EdicaoAlura.Interfaces
{
    public interface IDespesaRepository
    {
        DespesaModel CreateDespesa(PostDespesaDto createDespesaDto);
        void DeleteDespesa(int id);
        List<ReadDespesaDto> GetDespesaByDate(int ano, int mes);
        List<ReadDespesaDto> GetDespesaByDescricao(string descricao);
        ReadDespesaDto GetDespesaById(int id);
        List<ReadDespesaDto> GetDespesas();
        void PutDespsa(int id, PutDespesaDto putDespesaDto);
    }
}
