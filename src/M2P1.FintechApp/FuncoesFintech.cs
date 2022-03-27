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
            dynamic conta = _contaRepository.RetornarDado(id);

            if (tipoTransacao == TipoTransacaoEnum.Saque)
            {
                if (conta.GetType() == typeof(ContaCorrente))
                {
                    if (valor <= conta.ValorSaldo)
                    {
                        conta.Saque(valor);

                        Transacao transacao = new Transacao(TipoTransacaoEnum.Saque, "Saque", valor);
                        _contaRepository.AdicionarTransacao(id, transacao);

                        Console.WriteLine($"Saque na conta {id} de {conta.Nome} realizada");
                    }
                    else
                    {
                        if (valor > conta.ValorSaldo + conta.ValorChequeEspecial)
                        {
                            Console.WriteLine("Limite do cheque especial ultrapassado!");
                        }
                        else
                        {
                            conta.Saque(conta.ValorSaldo);
                            conta.UsoChequeEspecial(valor - conta.ValorSaldo);

                            Transacao transacao = new Transacao(TipoTransacaoEnum.Saque, "Saque", valor);
                            _contaRepository.AdicionarTransacao(id, transacao);

                            Console.WriteLine($"Saque na conta {id} de {conta.Nome} realizada, cheque especial utilizado!");
                        }
                    }
                }
                if (conta.GetType() == typeof(ContaPoupanca) || conta.GetType() == typeof(ContaInvestimento))
                {
                    if (valor <= conta.ValorSaldo)
                    {
                        conta.Saque(valor);

                        Transacao transacao = new Transacao(TipoTransacaoEnum.Saque, "Saque", valor);
                        _contaRepository.AdicionarTransacao(id, transacao);

                        Console.WriteLine($"Saque na conta {id} de {conta.Nome} realizada");
                    }
                    else
                    {
                        Console.WriteLine("Limite do saldo ultrapassado!");
                    }

                }
            }

            if (tipoTransacao == TipoTransacaoEnum.Deposito)
            {
                Transacao transacao = new Transacao(TipoTransacaoEnum.Deposito, "Depósito", valor);
                _contaRepository.AdicionarTransacao(id, transacao);

                conta.Deposito(valor);

                Console.WriteLine($"Depósito na conta {id} de {conta.Nome} realizada");
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

            Console.WriteLine($"Transferência da conta {idOrigem} de {contaOrigem.Nome} para conta {idDestino} de {contaDestino.Nome} realizada");
        }

        public void CriarContaPoupanca(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            Console.WriteLine($"Criando conta poupança numero {id}");

            ContaPoupanca conta = new ContaPoupanca(id, nome, cpf, endereco, rendaMensal, agencia);
            _contaRepository.AdicionarDado(conta);

            Console.WriteLine($"Conta poupança numero {id} criada com sucesso");
        }

        public void CriarContaCorrente(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            Console.WriteLine($"Criando conta corrente numero {id}");

            ContaCorrente conta = new ContaCorrente(id, nome, cpf, endereco, rendaMensal, agencia);
            _contaRepository.AdicionarDado(conta);

            Console.WriteLine($"Conta corrente numero {id} criada com sucesso");
        }

        public void AplicarRendimento(string id)
        {

            dynamic conta = _contaRepository.RetornarDado(id);
            conta.Rendimento();

            Console.WriteLine($"Aplicado rendimento na conta numero {id}");
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

        public void RetornarTransferencias()
        {
            Console.WriteLine("Todas as transferencias:");

            IList<Transferencia> list = _transferenciaRepository.RetornarDados();

            foreach (Transferencia transferencia in list)
            {
                Console.WriteLine($"Origem: {transferencia.DadosContaOrigem.Nome}, Destino: {transferencia.DadosContaDestino.Nome}, Valor: {transferencia.Valor}");
            }

        }
    }
}
