
namespace banco.InterfaceRepository
{
    public interface IRepository<T>
    {
        Task<bool> Inserir(T dado);
    }
}
