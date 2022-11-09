using ChallengeBackend4EdicaoAlura.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend4EdicaoAlura.Dtos.Despesas
{
    public class PutDespesaDto
    {
        [Required]
        public string Descricao { get; set; }

        public CategoriaDespesa Categoria { get; set; } = CategoriaDespesa.Outras;

        [Required]
        [DataType(DataType.Currency)]
        [Range(10, 1000000, ErrorMessage = "O valor deve estar entre 10 e 1000000")]
        public decimal Valor { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
