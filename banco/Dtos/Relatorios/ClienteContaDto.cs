using banco.DtosCliente;
using banco.DtosConta;

namespace banco.DtosRelatorioClienteConta
{
    public class ClienteContaDto
    {
        public ClienteDto Cliente { get; set; }

        public ContaDto Conta { get; set; }

        public ClienteContaDto(ClienteDto clienteDto, ContaDto contaDto)
        {
            Cliente = clienteDto;
            Conta = contaDto;
        }
    }
}
