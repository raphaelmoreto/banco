using banco.DtosConta;

namespace banco.InterfacesContaRepository
{
    public interface IContaRepository
    {
        Task<IEnumerable<ContaDto>> BuscarContas();
    }
}
