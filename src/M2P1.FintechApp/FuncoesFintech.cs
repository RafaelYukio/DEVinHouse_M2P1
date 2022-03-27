using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;

namespace M2P1.FintechApp
{
    public class FuncoesFintech
    {
        private readonly IContaRepository _contaRepository;
        private readonly ITransferenciaRepository _transferenciaRepository;

        public FuncoesFintech(IContaRepository ContaRepository, ITransferenciaRepository TransferenciaRepository)
        {
            _contaRepository = ContaRepository;
            _transferenciaRepository = TransferenciaRepository;
        }

        public void AdicionarTransacaoConta(string id, TipoTransacaoEnum tipoTransacao, decimal valor)
        {
            if (tipoTransacao == TipoTransacaoEnum.Saque)
            {
                Transacao transacao = new Transacao(TipoTransacaoEnum.Saque, "Saque", valor);
                _contaRepository.AdicionarTransacao(id, transacao);
                _contaRepository.RetornarDado(id).Saque(valor);
            }

            if (tipoTransacao == TipoTransacaoEnum.Deposito)
            {
                Transacao transacao = new Transacao(TipoTransacaoEnum.Deposito, "Depósito", valor);
                _contaRepository.AdicionarTransacao(id, transacao);
                _contaRepository.RetornarDado(id).Deposito(valor);

            }
        }

        public void AdicionarTransferencia(string idOrigem, string idDestino, decimal valor)
        {
            Conta contaOrigem = _contaRepository.RetornarDado(idOrigem);
            Conta contaDestino = _contaRepository.RetornarDado(idDestino);

            contaOrigem.Saque(valor);
            contaDestino.Deposito(valor);

            Transferencia transferecia = new Transferencia(TipoTransacaoEnum.Transferencia, "Transferencia", valor, contaOrigem, contaDestino);

            _contaRepository.AdicionarTransacao(idOrigem, transferecia);
            _contaRepository.AdicionarTransacao(idDestino, transferecia);
            _transferenciaRepository.AdicionarDado(transferecia);
        }

        public void CriarContaPoupanca(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            Console.WriteLine($"Criando conta numero {id}");

            ContaPoupanca conta = new ContaPoupanca(id, nome, cpf, endereco, rendaMensal, agencia);
            _contaRepository.AdicionarDado(conta);

            Console.WriteLine($"Conta numero {id} criada com sucesso");
        }

        public void RetornarContas()
        {
            Console.WriteLine("Todas as contas:");

            IList<Conta> list = _contaRepository.RetornarDados();

            foreach (Conta conta in list)
            {
                Console.WriteLine($"Nome: {conta.Nome}, Endereco: {conta.Endereco}, Saldo: {conta.Saldo()}");
            }

        }
    }
}
