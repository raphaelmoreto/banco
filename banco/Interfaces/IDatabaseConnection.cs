using System.Data;

namespace banco.InterfacesDatabase
{
    public interface IDatabaseConnection
    {
        IDbConnection GetConnection();
    }
}
