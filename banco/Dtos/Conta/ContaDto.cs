using banco.ModelsEnumsConta;

namespace banco.DtosConta
{
    public class ContaDto
    {
        public int IdCliente { get; set; }

        public TipoConta TipoConta { get; set; }

        public decimal Saldo { get; set; }

        public ContaDto(int idCliente, TipoConta tipoConta, decimal saldo)
        {
            IdCliente = idCliente;
            TipoConta = tipoConta;
            Saldo = saldo;
        }
    }
}
