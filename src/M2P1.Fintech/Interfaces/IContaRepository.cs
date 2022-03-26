using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Interfaces
{
    public interface IContaRepository
    {
        public void Sacar(string id, decimal valor);
        public void Depositar(string id, decimal valor);
        public decimal RetornarSaldo(string id);
        public IList<Transacao> RetornarExtrato(string id);
        public void Transferir(string idOrigem, string idDestino, decimal valor);
        public void AlterarDados(string id, string endereco, decimal rendaMensal, AgenciaEnum agencia);

    }
}
