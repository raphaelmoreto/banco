
namespace banco.InterfaceLayoutService
{
    public interface ILayoutService<T>
    {
        Task<List<T>> LerCsv(string caminhoArquivo);

        Task<List<T>> LerTxt(string caminhoArquivo);

        Task<List<T>> LerXlsx(string caminhoArquivo);
    }
}
