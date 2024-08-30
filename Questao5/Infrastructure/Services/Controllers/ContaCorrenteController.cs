using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{numeroConta}")]
        public async Task<ActionResult<ContaCorrenteQueryResponse>> GetSaldoContaCorrente(int numeroConta, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new ContaCorrenteQueryRequest(numeroConta), cancellationToken);
                if (result == null)
                {
                    return NotFound(new { Message = "Conta corrente não encontrada." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Erro ao processar a solicitação.",
                    Detail = ex.Message,
                    Status = 500
                });
            }
        }
    }
}
