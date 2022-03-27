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

        public void CriarContaInvestimento(string id, string nome, string cpf, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            Console.WriteLine($"Criando conta Investimento numero {id}");

            ContaInvestimento conta = new ContaInvestimento(id, nome, cpf, endereco, rendaMensal, agencia);
            _contaRepository.AdicionarDado(conta);

            Console.WriteLine($"Conta investimento numero {id} criada com sucesso");
        }

        public void SaqueConta(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            if (conta.GetType() == typeof(ContaCorrente))
            {
                if (valor <= Math.Max(0, conta.ValorSaldo) + conta.ValorChequeEspecial)
                {
                    if (valor > conta.ValorSaldo)
                    {
                        conta.UsoChequeEspecial(valor - Math.Max(0, conta.ValorSaldo));
                        conta.Saque(valor);

                        Console.WriteLine($"Saque na conta {id} de {conta.Nome} realizada, cheque especial utilizado!");
                    }
                    else
                    {
                        conta.Saque(valor);

                        Console.WriteLine($"Saque na conta {id} de {conta.Nome} realizada");
                    }
                    _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Saque, "Saque", valor));

                }
                else
                {
                    if (valor > Math.Max(0, conta.ValorSaldo) + conta.ValorChequeEspecial)
                    {
                        Console.WriteLine("Limite do cheque especial ultrapassado!");
                    }
                }
            }
            if (conta.GetType() == typeof(ContaPoupanca) || conta.GetType() == typeof(ContaInvestimento))
            {
                if (valor <= conta.ValorSaldo)
                {
                    conta.Saque(valor);

                    _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Saque, "Saque", valor));

                    Console.WriteLine($"Saque na conta {id} de {conta.Nome} realizada");
                }
                else
                {
                    Console.WriteLine("Limite do saldo ultrapassado!");
                }
            }
        }

        public void DepositoConta(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            conta.Deposito(valor);

            _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Deposito, "Depósito", valor));

            Console.WriteLine($"Depósito na conta {id} de {conta.Nome} realizada");
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

        public void CalcularRendimentoPoupanca(string id, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            conta.SimulacaoRendimento(conta.ValorAplicacoPoupanca, dataResgate);

            Console.WriteLine($"Aplicado rendimento na conta numero {id}");
        }

        public void SimularRendimentoPoupanca(string id, decimal valor, DateOnly dataResgate, decimal rendimento)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            decimal valorSimulacaoRendimentoPoupanca = conta.SimulacaoRendimento(valor, dataResgate, rendimento);

            Console.WriteLine($"Rendimento simulado na conta numero {id}, valor de {valorSimulacaoRendimentoPoupanca}");
        }

        public void AplicarPoupanca(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            conta.AplicarPoupanca(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCI", TipoAplicacoEnum.Poupanca, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void ResgatarPoupanca(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            conta.ResgatarPoupanca(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate Poupança", TipoAplicacoEnum.Poupanca, valor);
            investimento.Resgatar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void SimularRendimentoLCI(string id, decimal valor, DateOnly dataAplicacao, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorSimulacaoRendimentoLCI = conta.SimulacaoRendimentoLCI(valor, dataAplicacao, dataResgate);

            Console.WriteLine($"Rendimento simulado na conta numero {id}, valor de {valorSimulacaoRendimentoLCI}");
        }

        public void AplicarLCI(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            conta.AplicarLCI(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCI", TipoAplicacoEnum.LCI, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void AplicarLCA(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            conta.AplicarLCA(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCA", TipoAplicacoEnum.LCA, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void AplicarCDB(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            conta.AplicarCDB(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCA", TipoAplicacoEnum.CDB, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void ResgatarLCI(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            conta.ResgatarLCI(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate LCI", TipoAplicacoEnum.LCI, valor);
            investimento.Resgatar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void ResgatarLCA(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            conta.ResgatarLCA(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate LCA", TipoAplicacoEnum.LCA, valor);
            investimento.Resgatar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void ResgatarCDB(string id, decimal valor)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            conta.ResgatarCDB(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate CDB", TipoAplicacoEnum.CDB, valor);
            investimento.Resgatar();

            _contaRepository.AdicionarTransacao(id, investimento);
        }

        public void RetornarContas()
        {
            IList<Conta> list = _contaRepository.RetornarDados();

            decimal valorPoupanca;
            decimal valorLCI;
            decimal valorLCA;
            decimal valorCDB;

            Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|", "ID", "Tipo de conta", "Nome", "Endereço", "Saldo", "Cheque Especial", "Poupanca", "LCI", "LCA", "CDB"));

            foreach (dynamic conta in list)
            {
                if (conta.GetType() == typeof(ContaCorrente))
                {
                    Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|", conta.Id, conta.TipoConta, conta.Nome, conta.Endereco, conta.Saldo(), conta.LimiteChequeEspecial(), "-", "-", "-", "-"));
                }
                if (conta.GetType() == typeof(ContaPoupanca))
                {
                    Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|", conta.Id, conta.TipoConta, conta.Nome, conta.Endereco, conta.Saldo(), "-", conta.ValorPoupanca(), "-", "-", "-"));
                }
                if (conta.GetType() == typeof(ContaInvestimento))
                {
                    Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|", conta.Id, conta.TipoConta, conta.Nome, conta.Endereco, conta.Saldo(), "-", "-", conta.ValorLCI(), conta.ValorLCA(), conta.ValorCDB()));
                }
            }


        }

        public void RetornarTransacoesConta(string id)
        {
            IList<Transacao> list = _contaRepository.RetornarExtrato(id);

            foreach (dynamic transacao in list)
            {
                if (transacao.GetType() == typeof(Transacao))
                {
                    Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: {transacao.Valor}, Data: {transacao.Data}");
                }
                if (transacao.GetType() == typeof(Transferencia))
                {
                    Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: {transacao.Valor}, Destinho: {transacao.DadosContaDestino.Nome}, Data: {transacao.Data}");
                }

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
