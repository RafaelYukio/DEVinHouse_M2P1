using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class ContaCorrente : Conta
    {
        public decimal ValorChequeEspecial { get; private set; }
        public decimal LimiteChequeEspecial { get; private set; }

        public ContaCorrente(string id, string nome, string cpf, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia)
        : base(id, nome, cpf, endereco, rendaMensal, contaNumero, agencia)
        {
            TipoConta = TipoContaEnum.Corrente;
            LimiteChequeEspecial = rendaMensal * 0.10M;
            ValorChequeEspecial = LimiteChequeEspecial;
        }

        public void UsoChequeEspecial(decimal valor)
        {
            ValorChequeEspecial -= valor;
        }
        public void PagoChequeEspecial(decimal valor)
        {
            ValorChequeEspecial += valor;
        }
        public decimal RetornarLimiteChequeEspecial() => LimiteChequeEspecial;
        public decimal RetornarValorChequeEspecial() => ValorChequeEspecial;
        public void AlterarLimiteChequeEspecial(decimal rendaMensalNova) => LimiteChequeEspecial = rendaMensalNova * 0.10M;
    }
}
