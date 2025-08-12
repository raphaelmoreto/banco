
namespace banco.InterfacesImportarArquivo
{
    public interface IImportarArquivo<T>
    {
        Task<List<T>> LerCsv(string caminhoArquivo);

        Task<List<T>> LerTxt(string caminhoArquivo);

        Task<List<T>> LerXlsx(string caminhoArquivo);
    }
}
