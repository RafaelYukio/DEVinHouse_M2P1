using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class ContaCorrente : Conta
    {
        public decimal ValorChequeEspecial { get; private set; }

        public ContaCorrente(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        : base(id, nome, cpf, endereco, rendaMensal, agencia)
        {
            TipoConta = TipoContaEnum.Corrente;
            ValorChequeEspecial = Decimal.Multiply(rendaMensal, 0.10M);
        }

        public void UsoChequeEspecial(decimal valor)
        {
            ValorChequeEspecial = ValorChequeEspecial - valor;
        }

        public void PagoChequeEspecial(decimal valor)
        {
            ValorChequeEspecial = ValorChequeEspecial + valor;
        }
    }
}
