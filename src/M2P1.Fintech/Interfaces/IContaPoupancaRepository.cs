using M2P1.Fintech.Entidades;

namespace M2P1.Fintech.Interfaces
{
    public interface IContaPoupancaRepository : IBaseRepository<Conta>
    {
        decimal RetornarSaldo(string id);
    }
}
