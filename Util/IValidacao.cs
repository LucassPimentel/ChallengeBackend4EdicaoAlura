namespace ChallengeBackend4EdicaoAlura.Util
{
    public interface IValidacao
    {
        void ValidaSeJaExisteNoBancoDeDados<T>(string descricao, DateTime data);
        void ValidaSeAEntidadeENula<T>(int id);
        string ValidaOTipoDoDado<T>();
    }
}