using banco.InterfacesClienteRepository;
using banco.InterfacesImportarArquivo;
using banco.ModelsConta;
using banco.ModelsContaCorrente;
using banco.ModelsContaPoupanca;
using banco.ModelsEnumsConta;
using System.Globalization;
using OfficeOpenXml;

namespace banco.ServicesLayoutConta
{
    public class LayoutConta : IImportarArquivo<Conta>
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

                int idCliente = await _clienteRepository.RetornarIdDoClientePorCpf(dados[0]);
                TipoConta tipoConta = Enum.Parse<TipoConta>(dados[1]);

                decimal? saldo = decimal.TryParse(
                    dados[2],
                    NumberStyles.Number, //ACEITA NÚMEROS COM ESPAÇOS EM VOLTA, COM SINAL POSITIVO (+) OU NEGATIVO (-) E SEPARADO POR CASAS DECIMAIS
                    CultureInfo.InvariantCulture, //ACEITA O PONTO COMO SEPARADOR DE CASAS DECIMAIS
                    out decimal saldoParsed
                ) ? saldoParsed : 0.00m;

                decimal? saque = decimal.TryParse(
                    dados[3],
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal saqueParsed
                ) ? saqueParsed : 0.00m;

                Conta conta;

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

        public async Task<List<Conta>> LerTxt(string caminhoArquivo)
        {
            var contas = new List<Conta>();

            var linhas = await File.ReadAllLinesAsync(caminhoArquivo);
            foreach (var linha in linhas)
            {
                var dados = linha.Split('\t');
                if (dados.Length < 4)
                    continue;

                int idCliente = await _clienteRepository.RetornarIdDoClientePorCpf(dados[0]);
                TipoConta tipoConta = Enum.Parse<TipoConta>(dados[1]);

                decimal? saldo = decimal.TryParse(
                    dados[2],
                    NumberStyles.Number, //ACEITA NÚMEROS COM ESPAÇOS EM VOLTA, COM SINAL POSITIVO (+) OU NEGATIVO (-) E SEPARADO POR CASAS DECIMAIS
                    CultureInfo.InvariantCulture, //ACEITA O PONTO COMO SEPARADOR DE CASAS DECIMAIS
                    out decimal saldoParsed
                ) ? saldoParsed : 0.00m;

                decimal? saque = decimal.TryParse(
                    dados[3],
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal saqueParsed
                ) ? saqueParsed : 0.00m;

                Conta conta;

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

        public async Task<List<Conta>> LerXlsx(string caminhoArquivo)
        {
            var contas = new List<Conta>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var arquivo = new ExcelPackage( new FileInfo(caminhoArquivo)))
            {
                var planilha = arquivo.Workbook.Worksheets[0];
                var linhas = planilha.Dimension.End.Row;

                Conta conta;

                for (int linha = 1; linha <= linhas; linha++)
                {
                    int idCliente = await _clienteRepository.RetornarIdDoClientePorCpf(planilha.Cells[linha, 1].Text);
                    TipoConta tipoConta = Enum.Parse<TipoConta>(planilha.Cells[linha, 2].Text);

                    decimal? saldo = decimal.TryParse(
                        planilha.Cells[linha, 3].Text,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal saldoParced
                    ) ? saldoParced : 0.00m;

                    decimal? saque = decimal.TryParse(
                        planilha.Cells[linha, 4].Text,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal saqueParced
                    ) ? saqueParced : 0.00m;

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
            }
            return contas;
        }
    }
}
