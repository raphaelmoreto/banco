using System.Text;
using banco.InterfacesDatabase;
using banco.InterfaceRepository;
using banco.InterfaceClienteRepository;
using banco.ModelsCliente;
using banco.ModelsClienteEndereco;
using Dapper;

namespace banco.RepositorysCliente
{
    public class ClienteRepository : IRepository<Cliente>, IClienteRepository
    {
        private readonly IDatabaseConnection _dbConnection;

        public ClienteRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> Inserir(Cliente cliente)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[cliente] ([nome], [cpf])");
            sb.AppendLine("                           VALUES (@nome, @cpf)");

            var parameters = new
            {
                nome = cliente.Nome.ToUpper().Trim(),
                cpf = cliente.CPF.Trim()
            };

            var insercao = await connection.ExecuteAsync(sb.ToString(), parameters);
            return insercao > 0;
        }

        public async Task<bool> InsirirEnderecoCliente(Endereco endereco, string cpfCliente)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[endereco] ([rua], [numero], [bairro], [cidade], [estado], [cep], [fk_cliente])");
            sb.AppendLine("                               VALUES (@rua, @numero, @bairro, @cidade, @estado, @cep, @fk_cliente)");

            var paramentes = new
            {
                rua = endereco.Rua.ToUpper().Trim(),
                numero = endereco.Numero.ToUpper().Trim(),
                bairro = endereco.Bairro.ToUpper().Trim(),
                cidade = endereco.Cidade.ToUpper().Trim(),
                estado = endereco.Estado.ToUpper().Trim(),
                cep = endereco.CEP.Trim(),
                fk_cliente = await RetornarIdClientePorCpf(cpfCliente)
            };

            var retorno =  await connection.ExecuteAsync(sb.ToString(), paramentes);
            return retorno > 0;
        }

        public async Task<int> RetornarIdClientePorCpf(string clienteCpf)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id]");
            sb.AppendLine("FROM [dbo].[cliente]");
            sb.AppendLine("WHERE [cpf] = @cpf");

            var idCliente = await connection.ExecuteScalarAsync<int>(sb.ToString(), new { cpf = clienteCpf });
            return idCliente;
        }
    }
}
