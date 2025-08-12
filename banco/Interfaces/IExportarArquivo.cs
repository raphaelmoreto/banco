
namespace banco.InterfacesExportarArquivo
{
    public interface IExportarArquivo<T>
    {
        Task ExportarArquivoEmCsv(string caminho, IEnumerable<T> dados);

        Task ExportarArquivoEmTxt(string caminho, IEnumerable<T> dados);

        Task ExportarArquivoEmXlsx(string caminho, IEnumerable<T> dados);
    }
}