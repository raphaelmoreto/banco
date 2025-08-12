using banco.InterfacesClienteRepository;
using banco.InterfacesImportar;
using banco.InterfacesImportarArquivo;
using banco.InterfacesRepository;
using banco.ModelsCliente;

namespace banco.ServicesCliente
{
    public class ClienteService : IImportar
    {
        private readonly IImportarArquivo<Cliente> _layoutService;
        private readonly IRepository<Cliente> _repository;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IImportarArquivo<Cliente> layoutService, IRepository<Cliente> repositoryCliente, IClienteRepository clienteRepository)
        {
            _layoutService = layoutService;
            _repository = repositoryCliente;
            _clienteRepository = clienteRepository;
        }

        public async Task Importar(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                Console.WriteLine("ARQUIVO NÃO ENCONTRADO!");
                return;
            }

            try
            {
                string extensao = Path.GetExtension(caminhoArquivo).ToLower();
                List<Cliente> clientes = extensao switch
                {
                    ".csv" => await _layoutService.LerCsv(caminhoArquivo),
                    ".txt" => await _layoutService.LerTxt(caminhoArquivo),
                    ".xlsx" => await _layoutService.LerXlsx(caminhoArquivo),
                    _ => throw new NotSupportedException("A EXTENÇÃO DO ARQUIVO NÃO É SUPORTADA!")
                };

                foreach (Cliente cliente in clientes)
                {
                    var cadastroCliente = await _repository.Inserir(cliente);
                    if (!cadastroCliente)
                        continue;

                    if (cliente.Endereco != null)
                        await _clienteRepository.InserirEnderecoDoCliente(cliente.Endereco, cliente.CPF);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
        }
    }
}
