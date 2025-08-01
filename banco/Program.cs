using banco.Database;
using banco.ModelsCliente;
using banco.RepositorysCliente;
using banco.ServicesLayoutCliente;
using banco.InterfaceRepository;
using banco.InterfacesDatabase;
using banco.InterfaceClienteRepository;
using banco.InterfaceLayoutService;
using banco.ServicesCliente;

namespace banco
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-8LR8G0G\\SQLEXPRESS;Database=banco;Trusted_Connection=True;TrustServerCertificate=True;";
            IDatabaseConnection dbConnection = new DatabaseConnection(connectionString);

            ILayoutService<Cliente> layoutService = new LayoutCliente();
            IRepository<Cliente> repositoryCliente = new ClienteRepository(dbConnection);
            IClienteRepository clienteRepository = new ClienteRepository(dbConnection);
            ClienteService clienteService = new ClienteService(layoutService, repositoryCliente, clienteRepository);

            await clienteService.Importar("C:\\Users\\Raphael\\Documents\\PROJETOS\\C#\\banco\\Arquivos\\clientes.xlsx");
        }
    }
}
