using banco.DtosContasDeClientes;
using banco.DtosCliente;
using banco.DtosConta;
using banco.InterfacesClienteRepository;
using banco.InterfacesContaRepository;
using banco.InterfacesExportarArquivo;
using banco.ModelsEnumsConta;

namespace banco.ServicesRelatoriosContasDeClientes
{
    public class ContasDeClientes
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IExportarArquivo<ContasDeClietesDto> _exportarArquivo;

        public ContasDeClientes(IClienteRepository clienteRepository, IContaRepository contaRepository, IExportarArquivo<ContasDeClietesDto> exportarArquivo)
        {
            _clienteRepository = clienteRepository;
            _contaRepository = contaRepository;
            _exportarArquivo = exportarArquivo;
        }

        //PARA USAR A FUNÇÃO DEVE ENVIAR A EXTENSÃO DO ARQUIVO EM QUE DESEJA EXPORTAR ('.csv', '.txt', '.xlsx')
        public async Task Exportar(string extensao)
        {
            //MONTA DINAMICAMENTE O CAMINHO COMPLETO DA PASTA "Donwloads" DO USUÁRIO ATUAL DO WINDOWS
            string downloadsPath = Path.Combine(
                //"Environment" É UMA CLASSE DO .NET QUE DÁ ACESSO A INFORMAÇÕES DO SISTEMA, COMO VARIÁVEIS DE AMBIENTE E CAMINHOS DE PASTAS ESPECIAIS
                //"SpecialFolder.UserProfile" REPRESENTA A PASTA BASE DO USUÁRIO ATUAL E O "GetFolderPath" RETORNA ESSE CAMINHO COMO STRING
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );

            //RECEBE UMA LISTA DE CLIENTES E CONTAS
            var clientes = await _clienteRepository.BuscarClientes();
            var contas = await _contaRepository.BuscarContas();

            //CRIANDO UMA NOVA LISTA MESCLANDO 'clientes' e 'contas'
            var listaContasDeClientes = (
                from cl in clientes
                join ct in contas
                    on cl.Id equals ct.IdCliente
                select new ContasDeClietesDto(new ClienteDto(cl.Nome, cl.CPF), new ContaDto((TipoConta)ct.TipoConta, ct.Saldo))
            ).ToList();

            switch (extensao)
            {
                case "csv":
                    await _exportarArquivo.ExportarArquivoEmCsv(downloadsPath, listaContasDeClientes);
                    break;
                case "txt":
                    await _exportarArquivo.ExportarArquivoEmTxt(downloadsPath, listaContasDeClientes);
                    break;
                case "xlsx":
                    await _exportarArquivo.ExportarArquivoEmXlsx(downloadsPath, listaContasDeClientes);
                    break;
                default:
                    throw new NotSupportedException("EXTENSÃO DE ARQUIVO NÃO RECONHECIDA!");
            }

            throw new NotImplementedException();
        }
    }
}
