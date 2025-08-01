using banco.ModelsCliente;
using banco.ModelsClienteEndereco;

namespace banco.InterfaceClienteRepository
{
    public interface IClienteRepository
    {
        Task<bool> InsirirEnderecoCliente(Endereco endereco, string cpfCliente);

        Task<int> RetornarIdClientePorCpf(string clienteCpf);
    }
}
