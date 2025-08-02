using banco.ModelsConta;
using banco.ModelsEnumsConta;

namespace banco.ModelsContaCorrente
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente(int idCliente, TipoConta tipoConta, decimal? saldo, int? id = null) : base(idCliente, tipoConta, saldo, id)
        {
        }

        public override void Saque(decimal valor)
        {
            if (valor >= _valorMinimoSaque && valor <= Saldo)
                Saldo -= valor;
        }
    }
}
