using banco.ModelsEnumsConta;

namespace banco.ModelsConta
{
    public abstract class Conta
    {
        public int Id { get; protected set; }

        public int IdCliente { get; set; }

        public TipoConta TipoConta { get; set; }

        public decimal Saldo { get; protected set; }

        protected readonly decimal _valorMinimoSaque = 5.0m;

        public Conta(int idCliente, TipoConta tipoConta, decimal? saldo, int? id = null)
        {
            if (id.HasValue)
                Id = id.Value;

            if (saldo.HasValue)
                Saldo = saldo.Value;

            IdCliente = idCliente;
            TipoConta = tipoConta;
        }

        //'virtual' TEM UMA IMPLEMENTAÇÃO PADRÃO, MAS PODE SER SOBRESCRITO COM 'override' (NÃO É OBRIGATÓRIO SOBRESCREVER). USADO PARA PERMITIR QUE CLASSES DERIVADAS PERSONALIZEM O COMPORTAMENTO SE QUISEREM
        public virtual void Depositar(decimal valor)
        {
            Saldo += valor;
        }

        //'abstract' OBRIGATÓRIO SOBRESCREVER. NÃO TEM IMPLEMENTAÇÃO NO MÉTODO DA CLASSE BASE. CLASSES DERIVADAS DEVEM IMPLEMENTAR SEU PRÓPRIO COMPORTAMENTO
        public abstract void Saque(decimal valor);
    }
}
