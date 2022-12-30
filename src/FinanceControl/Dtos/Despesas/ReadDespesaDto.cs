using ChallengeBackend4EdicaoAlura.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend4EdicaoAlura.Dtos.Despesas
{
    public class ReadDespesaDto
    {
        public string Descricao { get; set; }

        public CategoriaDespesa Categoria { get; set; } = CategoriaDespesa.Outras;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }
}
