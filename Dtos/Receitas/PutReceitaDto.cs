using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend4EdicaoAlura.Dtos.Receitas
{
    public class PutReceitaDto
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        [Range(10, 1000000, ErrorMessage = "O valor deve estar entre 10 e 1000000")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
