
namespace banco.DtosCliente
{
    public class ClienteDto
    {
        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public ClienteDto(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }
    }
}
