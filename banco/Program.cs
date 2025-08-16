using banco.Database;
using banco.DtosContasDeClientes;
using banco.InterfacesClienteRepository;
using banco.InterfacesContaRepository;
using banco.InterfacesDatabase;
using banco.InterfacesExportarArquivo;
using banco.InterfacesImportarArquivo;
using banco.InterfacesRepository;
using banco.ModelsCliente;
using banco.ModelsConta;
using banco.RepositorysCliente;
using banco.RepositorysConta;
using banco.ServicesCliente;
using banco.ServicesConta;
using banco.ServicesLayoutCliente;
using banco.ServicesLayoutConta;
using banco.ServicesRelatoriosContasDeClientes;
using banco.ServicesRelatoriosExportarArquivos;

namespace banco
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-8LR8G0G\\SQLEXPRESS;Database=banco;Trusted_Connection=True;TrustServerCertificate=True;";
            IDatabaseConnection dbConnection = new DatabaseConnection(connectionString);

            //CLIENTE
            IImportarArquivo<Cliente> layoutService = new LayoutCliente();
            IRepository<Cliente> repository = new ClienteRepository(dbConnection);
            IClienteRepository clienteRepository = new ClienteRepository(dbConnection);
            ClienteService clienteService = new ClienteService(layoutService, repository, clienteRepository);

            //CONTA
            IImportarArquivo<Conta> layoutServiceConta = new LayoutConta(clienteRepository);
            IRepository<Conta> repositoryConta = new ContaRepository(dbConnection);
            ContaService contaService = new ContaService(layoutServiceConta, repositoryConta);

            //RELATÓRIO
            IContaRepository contaRepository = new ContaRepository(dbConnection);
            IExportarArquivo<ContasDeClietesDto> exportarArquivo = new ExportarArquivo();
            ContasDeClientes contasdeClientes = new ContasDeClientes(clienteRepository, contaRepository, exportarArquivo);

            await contasdeClientes.Exportar(".TXT");
        }
    }
}
