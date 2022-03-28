using M2P1.Fintech.Entidades;

namespace M2P1.Fintech.Interfaces
{
    public interface IBaseRepository<T> where T : BaseFintech
    {
        public DateTime RetornarDataRepository();
        public void MudarData(DateTime novaData);
        public void AdicionarDado(T dado);
        public void ApagarDado(string id);
        public T RetornarDado(string id);
        public IList<T> RetornarDados();
    }
}
