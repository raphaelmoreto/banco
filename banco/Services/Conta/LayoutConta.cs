using banco.InterfaceLayoutService;
using banco.InterfaceClienteRepository;
using banco.ModelsConta;
using banco.ModelsEnumsConta;
using banco.ModelsContaPoupanca;
using System.Globalization;
using banco.ModelsContaCorrente;

namespace banco.ServicesLayoutConta
{
    public class LayoutConta : ILayoutService<Conta>
    {
        private readonly IClienteRepository _clienteRepository;

        public LayoutConta(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Conta>> LerCsv(string caminhoArquivo)
        {
            var contas = new List<Conta>();

            var linhas = await File.ReadAllLinesAsync(caminhoArquivo);
            foreach (var linha in linhas.Skip(1))
            {
                var dados = linha.Split(';');
                if (dados.Length < 4)
                    continue;

                int idCliente = await _clienteRepository.RetornarIdClientePorCpf(dados[0]);
                TipoConta tipoConta = Enum.Parse<TipoConta>(dados[1]);

                decimal? saldo = decimal.TryParse(
                    dados[2],
                    NumberStyles.Number, //ACEITA NÚMEROS COM ESPAÇOS EM VOLTA, COM SINAL POSITIVO (+) OU NEGATIVO (-) E SEPARADO POR CASAS DECIMAIS
                    CultureInfo.InvariantCulture, //ACEITA O PONTO COMO SEPARADOR DE CASAS DECIMAIS
                    out decimal saldoParsed
                ) ? saldoParsed : 0.00m;

                Conta conta;

                decimal? saque = decimal.TryParse(
                    dados[3],
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal saqueParsed
                ) ? saqueParsed : 0.00m;

                if (tipoConta == TipoConta.poupanca)
                {
                    conta = new ContaPoupanca(idCliente, tipoConta, saldo);
                    if (saque.HasValue)
                        conta.Saque(saque.Value);
                }
                else if (tipoConta == TipoConta.corrente)
                {
                    conta = new ContaCorrente(idCliente, tipoConta, saldo);
                    if (saque.HasValue)
                        conta.Saque(saque.Value);
                }
                else
                {
                    continue; //TIPO DE CONTA NÃO IDENTIFICADA
                }
                contas.Add(conta);
            }
            return contas;
        }

        public Task<List<Conta>> LerTxt(string caminhoArquivo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Conta>> LerXlsx(string caminhoArquivo)
        {
            throw new NotImplementedException();
        }
    }
}
