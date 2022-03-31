using M2P1.Fintech.Entidades;
using M2P1.Fintech.Interfaces;

namespace M2P1.Fintech.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseFintech
    {
        public IList<T> Dados { get; private set; }
        public DateTime Data { get; private set; }

        public BaseRepository()
        {
            Dados = new List<T>();
            Data = DateTime.Now;
        }

        public DateTime RetornarData() => Data;
        public void MudarData(DateTime novaData)
        {
            Data = novaData;
        }

        public void AdicionarDado(T dado) => Dados.Add(dado);
        public void ApagarDado(string id) => Dados.Remove(RetornarDado(id));
        public T RetornarDado(string id) =>
                Dados.FirstOrDefault(dado => dado.Id == id)
             ?? throw new Exception($"Conta não encontrada!");
        public IList<T> RetornarDados() => Dados;
    }
}
