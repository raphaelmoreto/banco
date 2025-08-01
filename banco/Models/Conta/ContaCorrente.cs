using banco.ModelsConta;
using banco.ModelsCliente;
using banco.ModelsTipoConta;

namespace banco.ModelsContaCorrente
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente(Cliente cliente, TipoConta tipoConta, int? id, decimal? saldo) : base(cliente, tipoConta, id, saldo)
        {
        }

        public override void Saque(decimal valor)
        {
            Saldo -= valor;
        }
    }
}
