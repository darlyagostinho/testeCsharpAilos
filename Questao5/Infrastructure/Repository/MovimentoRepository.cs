using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Infrastructure.Repository
{
    public class MovimentoRepository : SqLiteRepository, IMovimentoRepository
    {
        public async Task<Movimento> AddMovimentoAsync(Movimento dados)
        {
            if (!File.Exists(DbFile)) return null;

            dados.IdMovimento = Guid.NewGuid().ToString();
            dados.DataMovimento = DateTime.Now.ToString("dd/MM/yyyy");

            using (var connection = DbConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                    VALUES (@IdMovimento, (SELECT idcontacorrente FROM contacorrente WHERE numero = @NumeroConta), @DataMovimento, @TipoMovimento, @Valor);
                    SELECT * FROM movimento WHERE idmovimento = @IdMovimento;";

                var parameters = new
                {
                    IdMovimento = dados.IdMovimento,
                    NumeroConta = dados.NumeroConta,
                    DataMovimento = dados.DataMovimento,
                    TipoMovimento = dados.TipoMovimento,
                    Valor = dados.Valor
                };

                return await connection.QueryFirstOrDefaultAsync<Movimento>(sql, parameters);
            }
        }

        public async Task<Movimento> GetMovimentoAsync(string IdMovimento)
        {
            if (!File.Exists(DbFile)) return null;

            using (var connection = DbConnection())
            {
                await connection.OpenAsync();

                var sql = "SELECT * FROM movimento WHERE idmovimento = @IdMovimento";
                var parameters = new { IdMovimento = IdMovimento };

                return await connection.QueryFirstOrDefaultAsync<Movimento>(sql, parameters);
            }
        }
    }
}
