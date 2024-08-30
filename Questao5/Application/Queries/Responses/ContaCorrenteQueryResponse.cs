namespace Questao5.Application.Queries.Responses
{
    public class ContaCorrenteQueryResponse
    {
        public int NumeroConta { get; set; }
        public string NomeTitular { get; set; }
        public DateTime DataHora { get; set; }
        public double Valor { get; set; }
    }
}
