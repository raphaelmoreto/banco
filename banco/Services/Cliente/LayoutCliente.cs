using banco.InterfaceLayoutService;
using banco.ModelsCliente;
using banco.ModelsClienteEndereco;
using OfficeOpenXml;
using System.Text;

namespace banco.ServicesLayoutCliente
{
    public class LayoutCliente : ILayoutService<Cliente>
    {
        public async Task<List<Cliente>> LerCsv(string caminhoArquivo)
        {
            var clientes = new List<Cliente>();

            var linhas = await File.ReadAllLinesAsync(caminhoArquivo, Encoding.UTF8);
            foreach (var linha in linhas.Skip(1)) //PULA O CABEÇALHO DO ARQUIVO CSV
            {
                var dados = linha.Split(';');
                if (dados.Length != 8)
                    continue;

                var cliente = new Cliente
                {
                    Nome = dados[0].ToUpper().Trim(),
                    CPF = dados[1].Trim(),
                    Endereco = new Endereco
                    {
                        Rua = dados[2].ToUpper().Trim(),
                        Numero = dados[3].Trim(),
                        Bairro = dados[4].ToUpper().Trim(),
                        Cidade = dados[5].ToUpper().Trim(),
                        Estado = dados[6].ToUpper().Trim(),
                        CEP = dados[7].Trim()
                    }
                };
                clientes.Add(cliente);
            }
            return clientes;
        }

        public async Task<List<Cliente>> LerTxt(string caminhoArquivo)
        {
            var clientes = new List<Cliente>();

            var linhas = await File.ReadAllLinesAsync(caminhoArquivo);
            foreach (var linha in linhas)
            {
                var dados = linha.Split('\t');
                if (dados.Length != 8)
                    continue;

                var cliente = new Cliente
                {
                    Nome = dados[0].ToUpper().Trim(),
                    CPF = dados[1].Trim(),
                    Endereco = new Endereco
                    {
                        Rua = dados[2].ToUpper().Trim(),
                        Numero = dados[3].Trim(),
                        Bairro = dados[4].ToUpper().Trim(),
                        Cidade = dados[5].ToUpper().Trim(),
                        Estado = dados[6].ToUpper().Trim(),
                        CEP = dados[7].Trim()
                    }
                };
                clientes.Add(cliente);
            }
            return clientes;
        }

        public Task<List<Cliente>> LerXlsx(string caminhoArquivo)
        {
            var clientes = new List<Cliente>();

            //INDICA QUE O USO DA BIBLIOTECA É NÃO-COMERCIAL
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var arquivo = new ExcelPackage( new FileInfo(caminhoArquivo)))
            {
                var planilha = arquivo.Workbook.Worksheets[0]; //SE NO ARQUIVO TIVER MAIS DE DUAS PLANILHAS, IRÁ ACESSAR A PRIMEIRA
                var linhas = planilha.Dimension.End.Row; //RETORNA O TOTAL DE LINHAS QUE HÁ NA PLANILHA

                for (int linha = 1; linha <= linhas; linha++) //COMEÇAR A LER DA PRIMEIRA LINHA DA PLANILHA
                {
                    var cliente = new Cliente
                    {
                        Nome = planilha.Cells[linha, 1].Text.ToUpper().Trim(),
                        CPF = planilha.Cells[linha, 2].Text.Trim(),
                        Endereco = new Endereco
                        {
                            Rua = planilha.Cells[linha, 3].Text.ToUpper().Trim(),
                            Numero = planilha.Cells[linha, 4].Text.Trim(),
                            Bairro = planilha.Cells[linha, 5].Text.ToUpper().Trim(),
                            Cidade = planilha.Cells[linha, 6].Text.ToUpper().Trim(),
                            Estado = planilha.Cells[linha, 7].Text.ToUpper().Trim(),
                            CEP = planilha.Cells[linha, 8].Text.Trim(),
                        }
                    };
                    clientes.Add(cliente);
                }
            }
            return Task.FromResult(clientes); //NÃO SEI PARA QUE SERVE. FALEI PARA O CHATGPT ME DAR A RESPOTA!
        }
    }
}
