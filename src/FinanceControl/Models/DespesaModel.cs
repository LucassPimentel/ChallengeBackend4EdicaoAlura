using ChallengeBackend4EdicaoAlura.Enums;

namespace ChallengeBackend4EdicaoAlura.Models
{
    public class DespesaModel
    {
        public int Id { get; set; }
        public CategoriaDespesa Categoria { get; set; } = CategoriaDespesa.Outras;
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

    }
}
