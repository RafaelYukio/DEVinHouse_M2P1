using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;

namespace M2P1.Fintech.Repositories
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        public void AdicionarTransacao(string id, Transacao transacao)
        {
            Conta conta = RetornarDado(id);
            conta.Historico.Add(transacao);
        }
        public decimal RetornarSaldo(string id) => RetornarDado(id).Saldo();
        public IList<Transacao> RetornarExtrato(string id) => RetornarDado(id).Extrato();
        public IList<Conta> RetornarContasPorTipo(Type tipoConta)
        {
            IList<Conta> contasPorTipo = new List<Conta>();
            IList<Conta> contas = RetornarDados();

            foreach (Conta conta in contas)
            {
                if (conta.GetType() == tipoConta)
                {
                    contasPorTipo.Add(conta);
                }
            }
            return contasPorTipo;

        }
        public IList<Conta> RetornarContasSaldoNegativo()
        {
            IList<Conta> contasSaldoNegativo = new List<Conta>();
            IList<Conta> contas = RetornarDados();

            foreach (Conta conta in contas)
            {
                decimal valorSaldo = conta.Saldo();

                if (valorSaldo < 0)
                {
                    contasSaldoNegativo.Add(conta);
                }
            }
            return contasSaldoNegativo;
        }
        public decimal RetornarTotalInvestido()
        {
            decimal totalInvestido = 0;
            IList<Conta> contas = RetornarDados();

            foreach (dynamic conta in contas)
            {
                if (conta.GetType() == typeof(ContaPoupanca))
                {
                    totalInvestido += conta.ValorPoupanca();
                }

                if (conta.GetType() == typeof(ContaInvestimento))
                {
                    totalInvestido += conta.ValorLCI() + conta.ValorLCA() + conta.ValorCDB();
                }
            }

            return totalInvestido;

        }
        public void AlterarDados(string nome, string id, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia)
        {
            Conta conta = RetornarDado(id);
            conta.Dados(nome, endereco, rendaMensal, contaNumero, agencia);
        }
    }
}
