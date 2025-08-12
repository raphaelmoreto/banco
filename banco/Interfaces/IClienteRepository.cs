using banco.DtosCliente;
using banco.ModelsClienteEndereco;

namespace banco.InterfacesClienteRepository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDto>> BuscarClientes();

        Task<bool> InserirEnderecoDoCliente(Endereco endereco, string cpfCliente);

        Task<int> RetornarIdDoClientePorCpf(string clienteCpf);
    }
}
