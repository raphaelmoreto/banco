using banco.ModelsConta;
using banco.ModelsEnumsConta;

namespace banco.ModelsContaPoupanca
{
    public class ContaPoupanca : Conta
    {
        private readonly decimal _taxaSaque = 3.50m; //O 'm' INFORMA QUE O VALOR E DO TIPO DECIMAL

        public ContaPoupanca(int idCliente, TipoConta tipoConta, decimal? saldo, int? id = null) : base(idCliente, tipoConta, saldo, id)
        {

        }

        public override void Saque(decimal valor)
        {
            if (valor >= _valorMinimoSaque && valor <= Saldo)
                Saldo -= (valor + _taxaSaque);
        }
    }
}
