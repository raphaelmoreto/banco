using banco.InterfacesContaRepository;
using banco.InterfacesDatabase;
using banco.InterfacesRepository;
using banco.DtosConta;
using banco.ModelsConta;
using Dapper;
using System.Text;

namespace banco.RepositorysConta
{
    public class ContaRepository : IRepository<Conta>, IContaRepository
    {
        private readonly IDatabaseConnection _dbConnection;

        public ContaRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<dynamic>> BuscarContas()
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [fk_cliente] AS 'IdCliente',");
            sb.AppendLine("           [fk_tipoConta] AS 'TipoConta',");
            sb.AppendLine("           [saldo] AS 'Saldo'");
            sb.AppendLine("FROM [dbo].[conta]");

            return await connection.QueryAsync<dynamic>(sb.ToString());
        }

        public async Task<bool> Inserir(Conta conta)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[conta] ([fk_cliente], [fk_tipoConta], [saldo])");
            sb.AppendLine("                           VALUES(@fkCliente, @fkTipoConta, @saldo)");

            var paramenters = new
            {
                fkCliente = conta.IdCliente,
                fkTipoConta = (int)conta.TipoConta,
                saldo = conta.Saldo,
            };

            var insercao = await connection.ExecuteAsync(sb.ToString(), paramenters);
            return insercao > 0;
        }
    }
}
