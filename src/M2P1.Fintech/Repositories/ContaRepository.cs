using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;

namespace M2P1.Fintech.Repositories
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        public void Sacar(string id, decimal valor)
        {
            Conta conta = RetornarDado(id);
            conta.Saque(valor);

            Transacao transacao = new Transacao(TipoTransacaoEnum.Despesa, "Saque", valor);
            conta.Historico.Add(transacao);

        }
        public void Depositar(string id, decimal valor)
        {
            Conta conta = RetornarDado(id);
            conta.Deposito(valor);

            Transacao transacao = new Transacao(TipoTransacaoEnum.Despesa, "Depósito", valor);
            conta.Historico.Add(transacao);
        }

        public decimal RetornarSaldo(string id) => RetornarDado(id).Saldo();

        public IList<Transacao> RetornarExtrato(string id) => RetornarDado(id).Extrato();

        public void Transferir(string idOrigem, string idDestino, decimal valor)
        {
            Conta contaOrigem = RetornarDado(idOrigem);
            Conta contaDestino = RetornarDado(idDestino);

            contaOrigem.Transferencia(TransferenciaEnum.Origem, valor);
            contaDestino.Transferencia(TransferenciaEnum.Destino, valor);
        }

        public void AlterarDados(string id, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            Conta conta = RetornarDado(id);
            conta.Dados(endereco, rendaMensal, agencia);
        }

    }
}
