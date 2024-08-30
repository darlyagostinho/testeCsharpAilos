using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<Movimento> AddMovimentoAsync(Movimento dados);
        Task<Movimento> GetMovimentoAsync(string idMovimento);
    }
}
