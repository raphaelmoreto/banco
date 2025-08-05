using banco.InterfacesDatabase;
using banco.InterfaceRepository;
using banco.ModelsConta;
using System.Text;
using Dapper;

namespace banco.RepositorysConta
{
    public class ContaRepository : IRepository<Conta>
    {
        private readonly IDatabaseConnection _dbConnection;

        public ContaRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
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
