using banco.Database;
using banco.ModelsCliente;
using banco.ModelsConta;
using banco.RepositorysCliente;
using banco.ServicesLayoutCliente;
using banco.InterfaceRepository;
using banco.InterfacesDatabase;
using banco.InterfaceClienteRepository;
using banco.InterfaceLayoutService;
using banco.ServicesCliente;
using banco.ServicesLayoutConta;
using banco.ServicesConta;
using banco.RepositorysConta;
using banco.ServicesRelatorios;
using banco.InterfaceContaRepository;
using banco.InterfaceExportar;
using banco.InterfaceExportarArquivo;
using banco.DtosRelatorioClienteConta;

namespace banco
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-8LR8G0G\\SQLEXPRESS;Database=banco;Trusted_Connection=True;TrustServerCertificate=True;";
            IDatabaseConnection dbConnection = new DatabaseConnection(connectionString);

            //CLIENTE
            ILayoutService<Cliente> layoutService = new LayoutCliente();
            IRepository<Cliente> repository = new ClienteRepository(dbConnection);
            IClienteRepository clienteRepository = new ClienteRepository(dbConnection);
            ClienteService clienteService = new ClienteService(layoutService, repository, clienteRepository);

            //CONTA
            ILayoutService<Conta> layoutServiceConta = new LayoutConta(clienteRepository);
            IRepository<Conta> repositoryConta = new ContaRepository(dbConnection);
            ContaService contaService = new ContaService(layoutServiceConta, repositoryConta);

            //RELATÓRIO
            IContaRepository contaRepository = new ContaRepository(dbConnection);
            IExportarArquivo<ClienteContaDto> exportarArquivo = new ExportarArquivo();
            IExportar relatorio = new ClienteConta(clienteRepository, contaRepository, exportarArquivo);

            await relatorio.Exportar("txt");
        }
    }
}
