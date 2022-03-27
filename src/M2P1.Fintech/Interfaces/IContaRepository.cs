using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Interfaces
{
    public interface IContaRepository : IBaseRepository<Conta>
    {
        public void AdicionarTransacao(string id, Transacao transacao);
        public decimal RetornarSaldo(string id);
        public IList<Transacao> RetornarExtrato(string id);
        public IList<Conta> RetornarContasPorTipo(Type tipoConta);
        public IList<Conta> RetornarContasSaldoNegativo();
        public decimal RetornarTotalInvestido();
        public void AlterarDados(string nome, string id, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia);
    }
}
