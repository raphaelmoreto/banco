using banco.InterfacesImportarArquivo;
using banco.InterfacesImportar;
using banco.InterfacesRepository;
using banco.ModelsConta;

namespace banco.ServicesConta
{
    public class ContaService : IImportar
    {
        private readonly IImportarArquivo<Conta> _layoutServiceConta;
        private readonly IRepository<Conta> _repository;

        public ContaService(IImportarArquivo<Conta> conta, IRepository<Conta> repository)
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
                var contas = extensao switch
                {
                    ".csv" => await _layoutServiceConta.LerCsv(caminhoArquivo),
                    ".txt" => await _layoutServiceConta.LerTxt(caminhoArquivo),
                    ".xlsx" => await _layoutServiceConta.LerXlsx(caminhoArquivo),
                    _ => throw new NotSupportedException("A EXTENÇÃO DO ARQUIVO NÃO É SUPORTADA!")
                };

                if (contas.Count >= 1)
                {
                    foreach (var conta in contas)
                    {
                        await _repository.Inserir(conta);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
        }
    }
}
