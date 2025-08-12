
namespace banco.InterfacesRepository
{
    public interface IRepository<T>
    {
        Task<bool> Inserir(T dado);
    }
}
