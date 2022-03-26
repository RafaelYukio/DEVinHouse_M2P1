using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;

namespace M2P1.FintechApp
{
    public class FuncoesFintech
    {
        private readonly IContaPoupancaRepository _ContaPoupancaRepository;

        public FuncoesFintech(IContaPoupancaRepository contaPoupancaRepository)
        {
            _ContaPoupancaRepository = contaPoupancaRepository;
        }

        public void CriarContaPoupanca(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            Console.WriteLine($"Criando conta numero {id}");

            ContaPoupanca conta = new ContaPoupanca(id, nome, cpf, endereco, rendaMensal, agencia);
            _ContaPoupancaRepository.AdicionarDado(conta);

            Console.WriteLine($"Conta numero {id} criada com sucesso");
        }

        public void RetornarContas()
        {
            Console.WriteLine("Todas as contas:");

            IList<Conta> list = _ContaPoupancaRepository.RetornarDados();

            foreach (Conta conta in list)
            {
                Console.WriteLine($"Nome: {conta.Nome}, Endereco: {conta.Endereco}");
            }

        }
    }
}
