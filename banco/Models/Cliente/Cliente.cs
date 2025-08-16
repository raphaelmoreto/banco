using banco.ModelsClienteEndereco;

namespace banco.ModelsCliente
{
    public class Cliente
    {
        public int Id { get; private set; }

        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public Endereco? Endereco { get; set; }

        public Cliente()
        {
        }

        public Cliente(string nome, string cpf, Endereco? endereco = null, int? id = null)
        {
            if (id.HasValue)
                Id = id.Value;

            if (endereco != null)
                Endereco = endereco;

            Nome = nome;
            CPF = cpf;
        }
    }
}
