using banco.ModelsConta;

namespace banco.InterfacesContaRepository
{
    public interface IContaRepository
    {
        Task<IEnumerable<dynamic>> BuscarContas();
    }
}
