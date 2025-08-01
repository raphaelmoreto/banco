
namespace banco.ModelsTipoConta
{
    public class TipoConta
    {
        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public TipoConta(string descricao, int? id)
        {
            if (id.HasValue)
                Id = id.Value;

            Descricao = descricao;
        }
    }
}
