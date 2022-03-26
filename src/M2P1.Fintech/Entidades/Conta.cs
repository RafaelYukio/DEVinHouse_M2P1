using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public abstract class Conta : BaseFintech
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Endereco { get; private set; }
        public decimal RendaMensal { get; private set; }
        public int ContaNumero { get; private set; }
        public AgenciaEnum Agencia { get; private set; }
        public decimal ValorSaldo { get; private set; }
        public IList<Transacao> Historico { get; private set; }

        protected Conta(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia) : base(id)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
            RendaMensal = rendaMensal;
            Agencia = agencia;
            ValorSaldo = 0;
            Historico = new List<Transacao>();
        }

        private Transacao AdicionarTransacao(TipoTransacaoEnum tipoTransacao, string descricao, decimal valor)
        {
            Transacao transacao = new Transacao(tipoTransacao, descricao, valor);
            return transacao;
        }

        public void Saque(decimal valor)
        {
            Historico.Add(AdicionarTransacao(TipoTransacaoEnum.Receita, "Saque", valor));
            ValorSaldo -= valor;
        }

        public void Deposito(decimal valor)
        {
            Historico.Add(AdicionarTransacao(TipoTransacaoEnum.Despesa, "Deposito", valor));
            ValorSaldo += valor;
        }

        public void Saldo()
        {

        }

        public void Extrato()
        {

        }

        public void Transferencia()
        {

        }

        public void AlterarDados()
        {

        }

    }
}
