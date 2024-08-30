using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetContaCorrenteAsync(int numeroConta);

        Task<double> GetValorAsync(int numeroConta);
    }
}
