using banco.ModelsCliente;
using banco.ModelsClienteEndereco;

namespace banco.InterfacesClienteRepository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> BuscarClientes();

        Task<bool> InserirEnderecoDoCliente(Endereco endereco, string cpfCliente);

        Task<int> RetornarIdDoClientePorCpf(string clienteCpf);
    }
}
