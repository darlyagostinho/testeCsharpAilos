using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string NumeroConta { get; private set; }
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public SituacaoConta Ativo { get; private set; }
    }
}
