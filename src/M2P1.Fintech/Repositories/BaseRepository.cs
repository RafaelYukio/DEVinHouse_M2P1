using M2P1.Fintech.Entidades;
using M2P1.Fintech.Interfaces;

namespace M2P1.Fintech.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseFintech
    {
        public IList<T> Dados { get; private set; }
        public BaseRepository() => Dados = new List<T>();
        public void AdicionarDado(T dado) => Dados.Add(dado);
        public void ApagarDado(string id) => Dados.Remove(RetornarDado(id));
        public T RetornarDado(string id) =>
                Dados.FirstOrDefault(dado => dado.Id == id)
             ?? throw new Exception($"Dado não encontrado");
        public IList<T> RetornarDados() => Dados;

    }
}
