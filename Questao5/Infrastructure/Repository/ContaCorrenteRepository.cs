using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Infrastructure.Repository
{
    public class ContaCorrenteRepository : SqLiteRepository, IContaCorrenteRepository
    {
        private const string ContaCorrenteQuery = @"
            SELECT idcontacorrente, numero, nome, ativo
            FROM contacorrente
            WHERE numero = @numeroConta";

        private const string MovimentoQuery = @"
            SELECT mv.TipoMovimento, mv.Valor
            FROM movimento mv
            INNER JOIN contacorrente cc
            ON mv.idcontacorrente = cc.idcontacorrente
            WHERE cc.numero = @numeroConta";

        public async Task<ContaCorrente> GetContaCorrenteAsync(int numeroConta)
        {
            if (!File.Exists(DbFile))
                return null;

            using (var connection = DbConnection())
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(
                    ContaCorrenteQuery, new { numeroConta });
            }
        }

        public async Task<double> GetValorAsync(int numeroConta)
        {
            if (!File.Exists(DbFile))
                return 0;

            using (var connection = DbConnection())
            {
                await connection.OpenAsync();
                var movimentos = await connection.QueryAsync<Movimento>(
                    MovimentoQuery, new { numeroConta });

                var valorCredito = movimentos.Where(e => e.TipoMovimento == "C").Sum(e => e.Valor);
                var valorDebito = movimentos.Where(e => e.TipoMovimento == "D").Sum(e => e.Valor);

                return valorCredito - valorDebito;
            }
        }
    }
}

