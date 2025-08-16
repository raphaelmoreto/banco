using banco.InterfacesImportarArquivo;
using banco.ModelsCliente;
using banco.ModelsClienteEndereco;
using OfficeOpenXml;
using System.Text;

namespace banco.ServicesLayoutCliente
{
    public class LayoutCliente : IImportarArquivo<Cliente>
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

                string rua = dados[2].ToUpper().Trim();
                string numero = dados[3].Trim();
                string bairro = dados[4].ToUpper().Trim();
                string cidade = dados[5].ToUpper().Trim();
                string estado = dados[6].ToUpper().Trim();
                string cep = dados[7].Trim();
                Endereco endereco = new Endereco(rua, numero, bairro, cidade, estado, cep);

                string nome = dados[0].ToUpper().Trim();
                string cpf = dados[1].Trim();
                Cliente cliente = new Cliente(nome, cpf, endereco);

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

                string rua = dados[2].ToUpper().Trim();
                string numero = dados[3].Trim();
                string bairro = dados[4].ToUpper().Trim();
                string cidade = dados[5].ToUpper().Trim();
                string estado = dados[6].ToUpper().Trim();
                string cep = dados[7].Trim();
                Endereco endereco = new Endereco(rua, numero, bairro, cidade, estado, cep);

                string nome = dados[0].ToUpper().Trim();
                string cpf = dados[1].Trim();
                Cliente cliente = new Cliente(nome, cpf, endereco);

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
                    string rua = planilha.Cells[linha, 3].Text.ToUpper().Trim();
                    string numero = planilha.Cells[linha, 4].Text.Trim();
                    string bairro = planilha.Cells[linha, 5].Text.ToUpper().Trim();
                    string cidade = planilha.Cells[linha, 6].Text.ToUpper().Trim();
                    string estado = planilha.Cells[linha, 7].Text.ToUpper().Trim();
                    string cep = planilha.Cells[linha, 8].Text.Trim();
                    Endereco endereco = new Endereco(rua, numero, bairro, cidade, estado, cep);

                    string nome = planilha.Cells[linha, 1].Text.ToUpper().Trim();
                    string cpf = planilha.Cells[linha, 2].Text.Trim();
                    Cliente cliente = new Cliente(nome, cpf, endereco);

                    clientes.Add(cliente);
                }
            }
            return Task.FromResult(clientes); //NÃO SEI PARA QUE SERVE. FALEI PARA O CHATGPT ME DAR A RESPOTA!
        }
    }
}
