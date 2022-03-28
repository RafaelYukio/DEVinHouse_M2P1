using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public abstract class Conta : BaseFintech
    {
        public TipoContaEnum TipoConta { get; protected set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Endereco { get; private set; }
        public decimal RendaMensal { get; private set; }
        public int ContaNumero { get; private set; }
        public AgenciaEnum Agencia { get; private set; }
        public decimal ValorSaldo { get; protected set; }
        public IList<Transacao> Historico { get; private set; }

        protected Conta(string id, string nome, string cpf, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia) : base(id)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
            RendaMensal = rendaMensal;
            ContaNumero = contaNumero;
            Agencia = agencia;
            ValorSaldo = 0;
            Historico = new List<Transacao>();
        }

        public void Saque(decimal valor)
        {
            ValorSaldo -= valor;
        }

        public void Deposito(decimal valor)
        {
            ValorSaldo += valor;
        }

        public decimal Saldo() => ValorSaldo;

        public IList<Transacao> Extrato() => Historico;

        public void AlteraDados(string nome, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia)
        {
            Nome = nome;
            Endereco = endereco;
            RendaMensal = rendaMensal;
            ContaNumero = contaNumero;
            Agencia = agencia;
        }

    }
}
