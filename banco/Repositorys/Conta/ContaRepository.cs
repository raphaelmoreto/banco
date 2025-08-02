using banco.InterfacesDatabase;
using banco.InterfaceRepository;
using banco.ModelsConta;

namespace banco.RepositorysConta
{
    public class ContaRepository : IRepository<Conta>
    {
        private readonly IDatabaseConnection _dbConnection;

        public ContaRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<bool> Inserir(Conta conta)
        {
            throw new NotImplementedException();
        }
    }
}
