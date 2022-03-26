using M2P1.Fintech.Entidades;
using M2P1.Fintech.Interfaces;

namespace M2P1.Fintech.Repositories
{
    public class ContaPoupancaRepository : ContaRepository, IContaPoupancaRepository
    {
        public decimal RetornarSaldo(string id) =>
            RetornarDado(id).Saldo();
    }
}
