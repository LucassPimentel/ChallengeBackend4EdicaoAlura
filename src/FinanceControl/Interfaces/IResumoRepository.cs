using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Dtos.Resumos;
using ChallengeBackend4EdicaoAlura.Enums;

namespace ChallengeBackend4EdicaoAlura.Interfaces
{
    public interface IResumoRepository
    {
        ReadResumoDto GerarResumo(int ano, int mes);

        decimal CalcularSaldoTotal(decimal receitaTotal, decimal despesaTotal);

        decimal GerarDespesaTotal(ReadResumoDto resumo, int ano, int mes);
    }
}
