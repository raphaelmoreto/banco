using banco.InterfaceExportarArquivo;
using banco.DtosRelatorioClienteConta;

namespace banco.ServicesRelatorios
{
    public class ExportarArquivo : IExportarArquivo<ClienteContaDto>
    {
        public Task ExportarArquivoEmCsv(string caminho, List<ClienteContaDto> dados)
        {
            throw new NotImplementedException();
        }

        public Task ExportarArquivoEmTxt(string caminho, List<ClienteContaDto> dados)
        {
            throw new NotImplementedException();
        }

        public Task ExportarArquivoEmXlsx(string caminho, List<ClienteContaDto> dados)
        {
            throw new NotImplementedException();
        }
    }
}
