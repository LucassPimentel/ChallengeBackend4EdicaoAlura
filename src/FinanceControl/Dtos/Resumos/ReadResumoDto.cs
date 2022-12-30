namespace ChallengeBackend4EdicaoAlura.Dtos.Resumos
{
    public class ReadResumoDto
    {
        public decimal ReceitaTotal { get; set; }
        public decimal DespesaTotal { get; set; }
        public decimal SaldoFinal { get; set; }
        public List<ReadGastoPorCategoriaDto> GastoPorCategoria { get; set; }

        public ReadResumoDto()
        {
            GastoPorCategoria = new List<ReadGastoPorCategoriaDto>();
        }
    }
}
