using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend4EdicaoAlura.Dtos.Receitas
{
    public class ReadReceitaDto
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

    }
}
