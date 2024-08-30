using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class ContaCorrenteQueryRequest : IRequest<ContaCorrenteQueryResponse>
    {
        public int NumeroConta { get; set; }
        public string Nome { get; set; }
        public string Idconta { get; set; }

        public ContaCorrenteQueryRequest(int numeroConta)
        {
            this.NumeroConta = numeroConta;
        }
    }
}
