using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;

namespace M2P1.FintechApp
{
    public class FuncoesFintech
    {
        private readonly IAcaoContaRepository _acaoContaRepository;

        public FuncoesFintech(IAcaoContaRepository acaoContaRepository)
        {
            _acaoContaRepository = acaoContaRepository;
        }

        public void CriarContaPoupanca(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            Console.WriteLine($"Criando conta numero {id}");

            ContaPoupanca conta = new ContaPoupanca(id, nome, cpf, endereco, rendaMensal, agencia);
            _acaoContaRepository.AdicionarDado(conta);

            Console.WriteLine($"Conta numero {id} criada com sucesso");
        }
    }
}
