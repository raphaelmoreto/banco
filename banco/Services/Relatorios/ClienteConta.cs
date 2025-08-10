using banco.InterfaceExportar;
using banco.InterfaceClienteRepository;
using banco.InterfaceContaRepository;
using banco.InterfaceExportarArquivo;
using banco.ModelsEnumsConta;
using banco.DtosRelatorioClienteConta;

namespace banco.ServicesRelatorios
{
    public class ClienteConta : IExportar
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IExportarArquivo<ClienteContaDto> _exportarArquivo;

        public ClienteConta(IClienteRepository clienteRepository, IContaRepository contaRepository, IExportarArquivo<ClienteContaDto> exportarArquivo)
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

            //CRIANDO UM NOVO ARQUIVO MESCLANDO 'clientes' e 'contas'
            var listaContasClientes = (
                from cl in clientes
                join ct in contas
                on cl.Id equals ct.IdCliente
                select new
                {
                    Cliente = cl.Nome,
                    CPF = cl.CPF,
                    Conta = Enum.Parse<TipoConta>(ct.TipoConta.ToString()),
                    Saldo = ct.Saldo
                }
            ).ToList();

            switch (extensao)
            {
                case "csv":
                    await _exportarArquivo.ExportarArquivoEmCsv(downloadsPath);
                    break;
                case "txt":
                    await _exportarArquivo.ExportarArquivoEmTxt(downloadsPath);
                    break;
                case "xlsx":
                    await _exportarArquivo.ExportarArquivoEmXlsx(downloadsPath);
                    break;
                default:
                    throw new NotSupportedException("EXTENSÃO DE ARQUIVO NÃO RECONHECIDA!");
            }

            throw new NotImplementedException();
        }
    }
}
