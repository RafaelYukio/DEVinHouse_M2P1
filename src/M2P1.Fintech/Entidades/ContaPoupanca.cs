using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class ContaPoupanca : Conta
    {
        public decimal ValorRendimento { get; private set; }        public ContaPoupanca(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
            : base(id, nome, cpf, endereco, rendaMensal, agencia)
        {
            TipoConta = TipoContaEnum.Poupanca;
            ValorRendimento = 1.05M;
        }
        public void Rendimento()
        {
            ValorSaldo = Decimal.Multiply(ValorSaldo, ValorRendimento);
        }
    }
}
