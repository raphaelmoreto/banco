using banco.DtosContasDeClientes;
using banco.InterfacesExportarArquivo;

namespace banco.ServicesRelatoriosExportarArquivos
{
    public class ExportarArquivo : IExportarArquivo<ContasDeClietesDto>
    {
        public Task ExportarArquivoEmCsv(string caminho, IEnumerable<ContasDeClietesDto> dados)
        {
            throw new NotImplementedException();
        }

        public Task ExportarArquivoEmTxt(string caminho, IEnumerable<ContasDeClietesDto> dados)
        {
            throw new NotImplementedException();
        }

        public Task ExportarArquivoEmXlsx(string caminho, IEnumerable<ContasDeClietesDto> dados)
        {
            throw new NotImplementedException();
        }
    }
}
