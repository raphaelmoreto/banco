using banco.DtosCliente;
using banco.DtosConta;

namespace banco.DtosContasDeClientes
{
    public class ContasDeClietesDto
    {
        public ClienteDto Cliente { get; set; }

        public ContaDto Conta { get; set; }

        public ContasDeClietesDto(ClienteDto clienteDto, ContaDto contaDto)
        {
            Cliente = clienteDto;
            Conta = contaDto;
        }
    }
}
