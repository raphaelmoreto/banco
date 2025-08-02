using banco.ModelsConta;
using banco.InterfaceImportarArquivo;
using banco.InterfaceLayoutService;
using banco.InterfaceRepository;

namespace banco.ServicesConta
{
    public class ContaService : IImportarArquivo
    {
        private readonly ILayoutService<Conta> _layoutServiceConta;
        private readonly IRepository<Conta> _repository;

        public ContaService(ILayoutService<Conta> conta, IRepository<Conta> repository)
        {
            _layoutServiceConta = conta;
            _repository = repository;
        }

        public async Task Importar(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                Console.WriteLine("ARQUIVO NÃO ENCONTRADO");
                return;
            }

            try
            {
                string extensao = Path.GetExtension(caminhoArquivo).ToLower();
                var conta = extensao switch
                {
                    ".csv" => await _layoutServiceConta.LerCsv(caminhoArquivo),
                    ".txt" => await _layoutServiceConta.LerTxt(caminhoArquivo),
                    ".xlsx" => await _layoutServiceConta.LerXlsx(caminhoArquivo),
                    _ => throw new NotSupportedException("A EXTENÇÃO DO ARQUIVO NÃO É SUPORTADA!")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
        }
    }
}
