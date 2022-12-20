using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ChallengeBackend4EdicaoAlura.Util
{
    public class Validacao : IValidacao
    {
        private readonly DataBaseContext _dbContext;
        public Validacao(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string ValidaOTipoDoDado<T>()
        {
            Type tipo = typeof(T);

            if (tipo.Name != "Receita" && tipo.Name != "Despesa")
            {
                throw new InvalidDataException("Tipo de dado não autorizado para validação.");
            }
            return tipo.Name;
        }
        public void ValidaSeAEntidadeENula<T>(int id)
        {
            dynamic? entidade;
            var tipo = ValidaOTipoDoDado<T>();

            if (tipo == "Receita") { entidade = _dbContext.Receitas.Find(id); }

            else { entidade = _dbContext.Despesas.Find(id); }

            if (entidade is null) { throw new KeyNotFoundException("Não encontrado..."); }

        }

        public void ValidaSeAListaEstaVazia<T>(List<T> lista)
        {
            if (lista.Count == 0) throw new ArgumentException("Desculpe, não encontramos nada.");
        }

        public void ValidaSeJaExisteNoBancoDeDados<T>(string descricao, DateTime data)
        {
            dynamic itens;
            var tipo = ValidaOTipoDoDado<T>();

            if (tipo == "Receita")
            {
                itens = _dbContext.Receitas.ToList();
                ValidaSeAListaEstaVazia<Receita>(itens);
            }
            else
            {
                itens = _dbContext.Despesas.ToList();
                ValidaSeAListaEstaVazia<Despesa>(itens);
            }

            foreach (var item in itens)
            {
                if (item.Descricao == descricao && item.Data.Month == data.Month)
                    throw new ArgumentException("Descrição e mês já existente.");
            }

        }
    }
}
