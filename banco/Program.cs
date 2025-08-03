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

namespace banco
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-8LR8G0G\\SQLEXPRESS;Database=banco;Trusted_Connection=True;TrustServerCertificate=True;";
            IDatabaseConnection dbConnection = new DatabaseConnection(connectionString);

            ILayoutService<Cliente> layoutService = new LayoutCliente();
            IRepository<Cliente> repository = new ClienteRepository(dbConnection);
            IClienteRepository clienteRepository = new ClienteRepository(dbConnection);
            ClienteService clienteService = new ClienteService(layoutService, repository, clienteRepository);

            ILayoutService<Conta> layoutServiceConta = new LayoutConta(clienteRepository);
            IRepository<Conta> repositoryConta = new ContaRepository(dbConnection);
            ContaService contaService = new ContaService(layoutServiceConta, repositoryConta);
            await contaService.Importar("C:\\Users\\Raphael\\Documents\\PROJETOS\\C#\\banco\\banco\\Arquivos\\contas.xlsx");
        }
    }
}
