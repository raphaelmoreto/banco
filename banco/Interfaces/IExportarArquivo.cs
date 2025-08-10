
namespace banco.InterfaceExportarArquivo
{
    public interface IExportarArquivo<T>
    {
        Task ExportarArquivoEmCsv(string caminho, List<T> dados);

        Task ExportarArquivoEmTxt(string caminho, List<T> dados);

        Task ExportarArquivoEmXlsx(string caminho, List<T> dados);
    }
}
