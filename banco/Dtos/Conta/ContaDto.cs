using banco.ModelsEnumsConta;

namespace banco.DtosConta
{
    public class ContaDto
    {
        public TipoConta TipoConta { get; set; }

        public decimal Saldo { get; set; }

        public ContaDto(TipoConta tipoConta, decimal saldo)
        {
            TipoConta = tipoConta;
            Saldo = saldo;
        }
    }
}