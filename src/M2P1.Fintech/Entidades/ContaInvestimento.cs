using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class ContaInvestimento : Conta
    {
    public ContaInvestimento(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
    : base(id, nome, cpf, endereco, rendaMensal, agencia)
        {
            TipoConta = TipoContaEnum.Investimento;
        }
    }
}
