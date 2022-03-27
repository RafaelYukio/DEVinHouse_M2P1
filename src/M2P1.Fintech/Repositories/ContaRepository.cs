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

        public void AlterarDados(string nome, string id, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia)
        {
            Conta conta = RetornarDado(id);
            conta.Dados(nome, endereco, rendaMensal, contaNumero, agencia);
        }
    }
}
