
namespace banco.ModelsClienteEndereco
{
    public class Endereco
    {
        public int Id { get; private set; }

        public string Rua { get; set; } = string.Empty;

        public string Numero { get; set; } = string.Empty;

        public string Bairro { get; set; } = string.Empty;

        public string Cidade { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public string CEP { get; set; } = string.Empty;

        public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep, int? id = null)
        {
            if (id.HasValue)
                Id = id.Value;

            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
        }
    }
}
