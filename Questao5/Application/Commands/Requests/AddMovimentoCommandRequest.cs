using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class AddMovimentoCommandRequest : IRequest<AddMovimentoCommandResponse>
    {
        public int NumeroConta { get; set; }
        public string TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
