using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class ContaCorrenteHandler : IRequestHandler<ContaCorrenteQueryRequest, ContaCorrenteQueryResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository ?? throw new ArgumentNullException(nameof(contaCorrenteRepository));
        }

        public async Task<ContaCorrenteQueryResponse> Handle(ContaCorrenteQueryRequest request, CancellationToken cancellationToken)
        {
            if (request.NumeroConta <= 0)
            {
                throw new ArgumentException("Conta inválida: INVALID_ACCOUNT");
            }

            var conta = await _contaCorrenteRepository.GetContaCorrenteAsync(request.NumeroConta);
            if (conta == null)
            {
                throw new InvalidOperationException("É permitido consultar o saldo apenas de conta corrente cadastrada. Conta Inválida: INVALID_ACCOUNT");
            }

            if (conta.Ativo == Domain.Enumerators.SituacaoConta.INATIVA)
            {
                throw new InvalidOperationException("É permitido consultar o saldo apenas de conta corrente ativa. Conta Inativa: INACTIVE_ACCOUNT");
            }

            var valor = await _contaCorrenteRepository.GetValorAsync(request.NumeroConta);

            return new ContaCorrenteQueryResponse
            {
                NumeroConta = conta.Numero,
                NomeTitular = conta.Nome,
                DataHora = DateTime.Now,
                Valor = valor
            };
        }
    }
}
