using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Dtos.Receitas;
using ChallengeBackend4EdicaoAlura.Dtos.Resumos;
using ChallengeBackend4EdicaoAlura.Enums;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Util;

namespace ChallengeBackend4EdicaoAlura.Repositories
{
    public class ResumoRepository : IResumoRepository
    {
        protected readonly IDespesaRepository _despesaRepository;
        protected readonly IReceitaRepository _receitaRepository;
        protected readonly DataBaseContext _dbContext;
        private readonly IValidacao _validacao;

        public ResumoRepository(IReceitaRepository receitaRepository, IDespesaRepository despesaRepository, DataBaseContext dbContext, IValidacao validacao)
        {
            _receitaRepository = receitaRepository;
            _despesaRepository = despesaRepository;
            _dbContext = dbContext;
            _validacao = validacao;
        }

        public void AdicionarGastosPorCategorias(ReadResumoDto resumo, IEnumerable<CategoriaDespesa> categorias, int ano, int mes)
        {
            var despesasPorData = _despesaRepository.GetDespesaByDate(ano, mes);


            foreach (var categoria in categorias)
            {
                var gastoPorCategoria = new ReadGastoPorCategoriaDto();

                gastoPorCategoria.Categoria = categoria;

                var despesaPorCategoria = despesasPorData.Where(c => c.Categoria == categoria).ToList();

                foreach (var despesa in despesaPorCategoria)
                {
                    gastoPorCategoria.Valor += despesa.Valor;
                }

                resumo.GastoPorCategoria.Add(gastoPorCategoria);
            }
        }

        public decimal CalcularSaldoTotal(decimal receitaTotal, decimal despesaTotal)
        {
            return receitaTotal - despesaTotal;
        }

        public decimal GerarDespesaTotal(ReadResumoDto resumo, int ano, int mes)
        {
            var despesasPorData = _despesaRepository.GetDespesaByDate(ano, mes);

            foreach (var despesa in despesasPorData)
            {
                resumo.DespesaTotal += despesa.Valor;
            }

            return resumo.DespesaTotal;

        }

        public decimal GerarReceitaTotal(ReadResumoDto resumo, int ano, int mes)
        {
            var receitasPorData = _receitaRepository.GetReceitaByDate(ano, mes);

            foreach (var receita in receitasPorData)
            {

                resumo.ReceitaTotal += receita.Valor;
            }

            return resumo.ReceitaTotal;

        }

        public ReadResumoDto GerarResumo(int ano, int mes)
        {
            var resumo = new ReadResumoDto();

            var receitaTotal = GerarReceitaTotal(resumo, ano, mes);

            var despesaTotal = GerarDespesaTotal(resumo, ano, mes);

            var saldoFinal = CalcularSaldoTotal(resumo.ReceitaTotal, resumo.DespesaTotal);

            var categorias = IdentificarCategorias(ano, mes);

            AdicionarGastosPorCategorias(resumo, categorias, ano, mes);

            resumo.ReceitaTotal = receitaTotal;
            resumo.DespesaTotal = despesaTotal;
            resumo.SaldoFinal = saldoFinal;

            return resumo;
        }

        public IEnumerable<CategoriaDespesa> IdentificarCategorias(int ano, int mes)
        {
            var despesasPorData = _despesaRepository.GetDespesaByDate(ano, mes);

            var categorias = despesasPorData.DistinctBy(c => c.Categoria).Select(d => d.Categoria);

            return categorias;

        }
    }
}
