using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Mapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class MovimentoHandler : IRequestHandler<AddMovimentoCommandRequest, AddMovimentoCommandResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoHandler(IMovimentoRepository movimentoRepository, IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentoRepository = movimentoRepository ?? throw new ArgumentNullException(nameof(movimentoRepository));
            _contaCorrenteRepository = contaCorrenteRepository ?? throw new ArgumentNullException(nameof(contaCorrenteRepository));
        }

        public async Task<AddMovimentoCommandResponse> Handle(AddMovimentoCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Valor <= 0)
            {
                throw new ArgumentException("São permitidos apenas valores maiores que zero: INVALID_VALUE");
            }

            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
            {
                throw new ArgumentException("Tipos permitidos são 'C' para crédito ou 'D' para débito: INVALID_TYPE");
            }

            var conta = await _contaCorrenteRepository.GetContaCorrenteAsync(request.NumeroConta);
            if (conta == null)
            {
                throw new InvalidOperationException("Conta corrente não encontrada ou inválida: INVALID_ACCOUNT");
            }

            if (conta.Ativo == SituacaoConta.INATIVA)
            {
                throw new InvalidOperationException("Somente conta ativa pode realizar movimentações. Conta corrente está inativa: INACTIVE_ACCOUNT");
            }

            var movimentoEntity = AppMapper.Mapper?.Map<Movimento>(request)
                ?? throw new InvalidOperationException("Mapper não configurado corretamente.");

            var addMovimento = await _movimentoRepository.AddMovimentoAsync(movimentoEntity);
            var movimento = await _movimentoRepository.GetMovimentoAsync(addMovimento.IdMovimento);

            if (movimento == null)
            {
                throw new InvalidOperationException("Falha ao recuperar o movimento adicionado: INVALID_MOVEMENT");
            }

            return new AddMovimentoCommandResponse
            {
                IdMovimento = addMovimento.IdMovimento
            };
        }
    }
}

