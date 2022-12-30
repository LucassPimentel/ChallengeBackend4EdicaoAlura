using ChallengeBackend4EdicaoAlura.Enums;

namespace ChallengeBackend4EdicaoAlura.Dtos.Resumos
{
    public class ReadGastoPorCategoriaDto
    {
        public CategoriaDespesa Categoria { get; set; }
        public decimal Valor { get; set; }
    }
}
