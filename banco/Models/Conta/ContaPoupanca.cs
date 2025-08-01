using banco.ModelsConta;
using banco.ModelsCliente;
using banco.ModelsTipoConta;

namespace banco.ModelsContaPoupanca
{
    public class ContaPoupanca : Conta
    {
        private readonly decimal _taxaSaque = 3.50m; //O 'm' INFORMA QUE O VALOR E DO TIPO DECIMAL

        public ContaPoupanca(Cliente cliente, TipoConta tipoConta, int? id, decimal? saldo) : base(cliente, tipoConta, id, saldo)
        {

        }

        public override void Saque(decimal valor)
        {
            Saldo -= (valor + _taxaSaque);
        }
    }
}
