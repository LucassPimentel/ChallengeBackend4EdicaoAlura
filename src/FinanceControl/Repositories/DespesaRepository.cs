using AutoMapper;
using ChallengeBackend4EdicaoAlura.Context;
using ChallengeBackend4EdicaoAlura.Dtos.Despesas;
using ChallengeBackend4EdicaoAlura.Interfaces;
using ChallengeBackend4EdicaoAlura.Models;
using ChallengeBackend4EdicaoAlura.Util;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeBackend4EdicaoAlura.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly DataBaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidacao _validacao;
        public DespesaRepository(DataBaseContext dbContext, IMapper mapper, IValidacao validacao)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validacao = validacao;
        }

        public DespesaModel CreateDespesa(PostDespesaDto createDespesaDto)
        {
            var despesa = _mapper.Map<DespesaModel>(createDespesaDto);

            _validacao.ValidaSeJaExisteNoBancoDeDados<DespesaModel>(createDespesaDto.Descricao, createDespesaDto.Data);

            _dbContext.Despesas.Add(despesa);
            _dbContext.SaveChanges();
            return despesa;
        }

        public void DeleteDespesa(int id)
        {
            var despesa = _dbContext.Despesas.Find(id);
            _validacao.ValidaSeAEntidadeENula<DespesaModel>(id);
            _dbContext.Despesas.Remove(despesa);
            _dbContext.SaveChanges();
        }

        public List<ReadDespesaDto> GetDespesaByDate(int ano, int mes)
        {
            var despesasPorData = _dbContext.Despesas.Where(d => d.Data.Year == ano && d.Data.Month == mes).ToList();

            var dtoDespesasPorData = _mapper.Map<List<ReadDespesaDto>>(despesasPorData);

            return dtoDespesasPorData;
        }

        public List<ReadDespesaDto> GetDespesaByDescricao(string descricao)
        {
            var despesasComPalavraChaveNaDescricao = _dbContext.Despesas.Where(d =>
            d.Descricao
            .ToUpper()
            .Contains(descricao.ToUpper())).ToList();

            var dtoDespesasComPalavraChaveNaDescricao = _mapper.Map<List<ReadDespesaDto>>(despesasComPalavraChaveNaDescricao);

            return dtoDespesasComPalavraChaveNaDescricao;

        }

        public ReadDespesaDto GetDespesaById(int id)
        {
            var despesa = _dbContext.Despesas.Find(id);
            _validacao.ValidaSeAEntidadeENula<DespesaModel>(id);
            var readDespesaDto = _mapper.Map<ReadDespesaDto>(despesa);
            return readDespesaDto;
        }

        public List<ReadDespesaDto> GetDespesas()
        {
            var despesas = _dbContext.Despesas.ToList();
            var readDespesasDto = _mapper.Map<List<ReadDespesaDto>>(despesas);
            return readDespesasDto;
        }

        public void PutDespsa(int id, PutDespesaDto putDespesaDto)
        {
            var despesa = _dbContext.Despesas.Find(id);

            _validacao.ValidaSeAEntidadeENula<DespesaModel>(id);
            _validacao.ValidaSeJaExisteNoBancoDeDados<DespesaModel>(putDespesaDto.Descricao, putDespesaDto.Data);

            _mapper.Map(putDespesaDto, despesa);

            _dbContext.SaveChanges();
        }

    }
}
