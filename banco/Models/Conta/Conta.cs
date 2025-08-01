using banco.ModelsCliente;
using banco.ModelsTipoConta;

namespace banco.ModelsConta
{
    public abstract class Conta
    {
        public int Id { get; protected set; }

        public required Cliente Cliente { get; set; }

        public required TipoConta TipoConta { get; set; }

        public decimal Saldo { get; protected set; }

        public Conta(Cliente cliente, TipoConta tipoConta, int? id, decimal? saldo)
        {
            if (id.HasValue)
                Id = id.Value;

            if (saldo.HasValue)
                Saldo = saldo.Value;

            Cliente = cliente;
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
