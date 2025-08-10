using banco.DtosCliente;
using banco.ModelsClienteEndereco;

namespace banco.InterfaceClienteRepository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDto>> BuscarClientes();

        Task<bool> InsirirEnderecoCliente(Endereco endereco, string cpfCliente);

        Task<int> RetornarIdClientePorCpf(string clienteCpf);
    }
}
