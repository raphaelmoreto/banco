using banco.DtosConta;

namespace banco.InterfaceContaRepository
{
    public interface IContaRepository
    {
        Task<IEnumerable<ContaDto>> BuscarContas();
    }
}
