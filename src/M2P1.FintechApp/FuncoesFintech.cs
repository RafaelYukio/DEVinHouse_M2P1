﻿using M2P1.Fintech.Entidades;
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

        public void RetornarData()
        {
            Console.WriteLine(_contaRepository.RetornarDataRepository());
            Console.WriteLine(_transferenciaRepository.RetornarDataRepository());
        }
        public void MudarData(DateTime novaData)
        {
            _contaRepository.MudarData(novaData);
            _transferenciaRepository.MudarData(novaData);

            IList<Conta> contas = _contaRepository.RetornarDados();

            foreach (dynamic conta in contas)
            {
                if (conta.GetType() == typeof(ContaPoupanca) || conta.GetType() == typeof(ContaInvestimento))
                {
                    conta.Render(DateOnly.FromDateTime(novaData));
                }
            }
        }
        public bool VerificaContaNumero(string contaNumero)
        {
            bool contaExistente;

            try
            {
                _contaRepository.RetornarDado(contaNumero);
                return contaExistente = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return contaExistente = false;
            }
        }
        public bool VerificaValorZero(decimal valor) => valor == 0 ? true : false;

        public void CriarContaPoupanca(string nome, string cpf, string endereco, decimal rendaMensal)
        {
            int qtdContas;
            IList<Conta> contas = _contaRepository.RetornarDados();

            qtdContas = contas.Count;
            int contaNumero = qtdContas + 1;
            string id = contaNumero.ToString();
            string nomeAgencia = "";

            Random random = new Random();

            Type tipo = typeof(AgenciaEnum);
            Array valoresEnums = tipo.GetEnumValues();
            int index = random.Next(valoresEnums.Length);

            AgenciaEnum agencia = (AgenciaEnum)valoresEnums.GetValue(index);

            ContaPoupanca conta = new ContaPoupanca(id, nome, cpf, endereco, rendaMensal, contaNumero, agencia);
            _contaRepository.AdicionarDado(conta);

            switch (agencia)
            {
                case AgenciaEnum.Biguacu:
                    nomeAgencia = "Biguaçu";
                    break;
                case AgenciaEnum.SaoJose:
                    nomeAgencia = "São José";
                    break;
                case AgenciaEnum.Florianopolis:
                    nomeAgencia = "Florianópolis";
                    break;
            }

            Console.WriteLine($"Conta poupança de número/ID {id}, em nome de {nome}, da agência {nomeAgencia}, criada com sucesso!");
        }
        public void CriarContaCorrente(string nome, string cpf, string endereco, decimal rendaMensal)
        {
            int qtdContas;
            IList<Conta> contas = _contaRepository.RetornarDados();

            qtdContas = contas.Count;
            int contaNumero = qtdContas + 1;
            string id = contaNumero.ToString();
            string nomeAgencia = "";

            Random random = new Random();

            Type tipo = typeof(AgenciaEnum);
            Array valoresEnums = tipo.GetEnumValues();
            int index = random.Next(valoresEnums.Length);

            AgenciaEnum agencia = (AgenciaEnum)valoresEnums.GetValue(index);

            ContaCorrente conta = new ContaCorrente(id, nome, cpf, endereco, rendaMensal, contaNumero, agencia);
            _contaRepository.AdicionarDado(conta);

            switch (agencia)
            {
                case AgenciaEnum.Biguacu:
                    nomeAgencia = "Biguaçu";
                    break;
                case AgenciaEnum.SaoJose:
                    nomeAgencia = "São José";
                    break;
                case AgenciaEnum.Florianopolis:
                    nomeAgencia = "Florianópolis";
                    break;
            }

            Console.WriteLine($"Conta corrente de número/ID {id}, em nome de {nome}, da agência {nomeAgencia}, criada com sucesso!");
        }
        public void CriarContaInvestimento(string nome, string cpf, string endereco, decimal rendaMensal)
        {
            int qtdContas;
            IList<Conta> contas = _contaRepository.RetornarDados();

            qtdContas = contas.Count;
            int contaNumero = qtdContas + 1;
            string id = contaNumero.ToString();
            string nomeAgencia = "";

            Random random = new Random();

            Type tipo = typeof(AgenciaEnum);
            Array valoresEnums = tipo.GetEnumValues();
            int index = random.Next(valoresEnums.Length);

            AgenciaEnum agencia = (AgenciaEnum)valoresEnums.GetValue(index);

            ContaInvestimento conta = new ContaInvestimento(id, nome, cpf, endereco, rendaMensal, contaNumero, agencia);
            _contaRepository.AdicionarDado(conta);

            switch (agencia)
            {
                case AgenciaEnum.Biguacu:
                    nomeAgencia = "Biguaçu";
                    break;
                case AgenciaEnum.SaoJose:
                    nomeAgencia = "São José";
                    break;
                case AgenciaEnum.Florianopolis:
                    nomeAgencia = "Florianópolis";
                    break;
            }

            Console.WriteLine($"Conta investimento de número/ID {id}, em nome de {nome}, da agência {nomeAgencia}, criada com sucesso!");
        }

        public void SaqueConta(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            if (conta.GetType() == typeof(ContaCorrente))
            {
                if (valor <= Math.Max(0, conta.Saldo()) + conta.ValorChequeEspecial)
                {
                    if (valor > conta.Saldo())
                    {
                        conta.UsoChequeEspecial(valor - Math.Max(0, conta.Saldo()));
                        Console.WriteLine($"Saque na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso, cheque especial utilizado!");
                    }
                    else
                    {
                        Console.WriteLine($"Saque na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
                    }
                    conta.Saque(valor);
                    _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Saque, "Saque", valor));
                }
                else
                {
                    Console.WriteLine("Limite do cheque especial ultrapassado!");
                }
            }
            else
            {
                if (valor <= conta.Saldo())
                {
                    conta.Saque(valor);
                    _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Saque, "Saque", valor));

                    Console.WriteLine($"Saque na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Limite do saldo ultrapassado!");
                }
            }
        }
        public void DepositoConta(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            decimal faltaPagarChequeEspecial = conta.RetornarLimiteChequeEspecial() - conta.RetornarValorChequeEspecial();

            if (conta.GetType() == typeof(ContaCorrente))
            {
                if (valor <= faltaPagarChequeEspecial)
                {
                    conta.PagoChequeEspecial(valor);

                    Console.WriteLine($"Depósito na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, utilizada para pagar cheque especial.");
                }
                else
                {
                    conta.PagoChequeEspecial(faltaPagarChequeEspecial);

                    Console.WriteLine($"Depósito na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
                }
                conta.Deposito(valor);
                _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Deposito, "Depósito", valor));

            }
            else
            {
                conta.Deposito(valor);
                _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Deposito, "Depósito", valor));

                Console.WriteLine($"Depósito na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
            }

        }
        public void TransferenciaConta(string idOrigem, string idDestino, decimal valor)
        {
            DayOfWeek data = DateTime.Now.DayOfWeek;

            if (data == DayOfWeek.Saturday || data == DayOfWeek.Sunday)
            {
                Console.WriteLine("Não é possível realizar transferência nos finais de semana!");
                return;
            }

            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            if (idOrigem == idDestino)
            {
                Console.WriteLine("Não é possível transferir para a mesma conta");
            }
            else
            {

                dynamic contaOrigem = _contaRepository.RetornarDado(idOrigem);
                Conta contaDestino = _contaRepository.RetornarDado(idDestino);

                if (contaOrigem.GetType() == typeof(ContaCorrente))
                {
                    if (valor <= Math.Max(0, contaOrigem.Saldo()) + contaOrigem.ValorChequeEspecial)
                    {
                        if (valor > contaOrigem.Saldo())
                        {
                            contaOrigem.UsoChequeEspecial(valor - Math.Max(0, contaOrigem.Saldo()));
                            contaOrigem.Saque(valor);

                            Console.WriteLine($"Transferência da conta número/ID {idOrigem} de {contaOrigem.Nome} para conta número/ID {idDestino} de {contaDestino.Nome}, no valor de R$ {valor}, realizada com sucesso! Cheque especial utilizado!");
                        }
                        else
                        {
                            contaOrigem.Saque(valor);

                            Console.WriteLine($"Transferência da conta número/ID {idOrigem} de {contaOrigem.Nome} para conta número/ID {idDestino} de {contaDestino.Nome}, no valor de R$ {valor}, realizada com sucesso!");
                        }

                        contaDestino.Deposito(valor);

                        Transferencia transferecia = new Transferencia(TipoTransacaoEnum.Transferencia, "Transferência", valor, contaOrigem, contaDestino);

                        _contaRepository.AdicionarTransacao(idOrigem, transferecia);
                        _contaRepository.AdicionarTransacao(idDestino, transferecia);
                        _transferenciaRepository.AdicionarDado(transferecia);

                    }
                    else
                    {
                        Console.WriteLine($"Transferência não realizada. Saldo (saldo + valor do cheque especial) insuficiente da conta número/ID {idOrigem} de {contaOrigem.Nome}");
                    }

                }
                else
                {
                    if (valor <= contaOrigem.Saldo())
                    {
                        contaOrigem.Saque(valor);

                        Transferencia transferecia = new Transferencia(TipoTransacaoEnum.Transferencia, "Transferência", valor, contaOrigem, contaDestino);

                        _contaRepository.AdicionarTransacao(idOrigem, transferecia);
                        _contaRepository.AdicionarTransacao(idDestino, transferecia);
                        _transferenciaRepository.AdicionarDado(transferecia);

                        Console.WriteLine($"Transferência da conta número/ID {idOrigem} de {contaOrigem.Nome} para conta {idDestino} de {contaDestino.Nome} realizada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine($"Transferência não realizada. Saldo insuficiente da conta número/ID {idOrigem} de {contaOrigem.Nome}");
                    }
                }

            }
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

            Console.WriteLine($"Valor aplicado hoje de R$ {valor}, será de R$ {valorSimulacaoRendimentoPoupanca.ToString("0.00")} em {dataResgate.ToString("yyyy/MM/dd")}");
        }
        public void AplicarPoupanca(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);
            conta.AplicarPoupanca(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCI", TipoAplicacoEnum.Poupanca, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);

            Console.WriteLine($"Aplicação na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
        }
        public void ResgatarPoupanca(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            if (valor > conta.RetornarValorPoupanca())
            {
                Console.WriteLine($"Falha no resgate, valor maior que o disponível!");
            }
            else
            {
                conta.ResgatarPoupanca(valor);

                Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate Poupança", TipoAplicacoEnum.Poupanca, valor);
                investimento.Resgatar();

                _contaRepository.AdicionarTransacao(id, investimento);

                Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
            }

        }

        public void SimularRendimentoLCI(string id, decimal valor, DateOnly dataAplicacao, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorSimulacaoRendimentoLCI = conta.SimulacaoRendimentoLCI(valor, dataAplicacao, dataResgate);

            Console.WriteLine($"Valor de R$ {valor} será R$ {valorSimulacaoRendimentoLCI}, se aplicado em LCI durante {dataAplicacao} a {dataResgate}");
        }
        public void SimularRendimentoLCA(string id, decimal valor, DateOnly dataAplicacao, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorSimulacaoRendimentoLCA = conta.SimulacaoRendimentoLCA(valor, dataAplicacao, dataResgate);

            Console.WriteLine($"Valor de R$ {valor} será R$ {valorSimulacaoRendimentoLCA}, se aplicado em LCI durante {dataAplicacao} a {dataResgate}");
        }
        public void SimularRendimentoCDB(string id, decimal valor, DateOnly dataAplicacao, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorSimulacaoRendimentoCDB = conta.SimulacaoRendimentoCDB(valor, dataAplicacao, dataResgate);

            Console.WriteLine($"Valor de R$ {valor} será R$ {valorSimulacaoRendimentoCDB}, se aplicado em LCI durante {dataAplicacao} a {dataResgate}");
        }
        public void AplicarLCI(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            conta.AplicarLCI(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCI", TipoAplicacoEnum.LCI, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);

            Console.WriteLine($"Aplicação na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
        }
        public void AplicarLCA(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            conta.AplicarLCA(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCA", TipoAplicacoEnum.LCA, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);

            Console.WriteLine($"Aplicação na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
        }
        public void AplicarCDB(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            conta.AplicarCDB(valor);

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCA", TipoAplicacoEnum.CDB, valor);
            investimento.Aplicar();

            _contaRepository.AdicionarTransacao(id, investimento);

            Console.WriteLine($"Aplicação na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
        }
        public void ResgatarLCI(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            if (valor > conta.RetornarValorLCI())
            {
                Console.WriteLine($"Falha no resgate, valor maior que o disponível!");
            }
            else
            {
                conta.ResgatarLCI(valor);

                Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate LCI", TipoAplicacoEnum.LCI, valor);
                investimento.Resgatar();

                _contaRepository.AdicionarTransacao(id, investimento);

                Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
            }
        }
        public void ResgatarLCA(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            if (valor > conta.RetornarValorLCA())
            {
                Console.WriteLine($"Falha no resgate, valor maior que o disponível!");
            }
            else
            {
                conta.ResgatarLCA(valor);

                Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate LCA", TipoAplicacoEnum.LCA, valor);
                investimento.Resgatar();

                _contaRepository.AdicionarTransacao(id, investimento);

                Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
            }
        }
        public void ResgatarCDB(string id, decimal valor)
        {
            if (VerificaValorZero(valor) == true)
            {
                Console.WriteLine("Valor da transação não pode ser zero!");
                return;
            }

            dynamic conta = _contaRepository.RetornarDado(id);

            if (valor > conta.RetornarValorCDB())
            {
                Console.WriteLine($"Falha no resgate, valor maior que o disponível!");
            }
            else
            {
                conta.ResgatarCDB(valor);

                Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate CDB", TipoAplicacoEnum.CDB, valor);
                investimento.Resgatar();

                _contaRepository.AdicionarTransacao(id, investimento);

                Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
            }
        }

        public Type RetornarTipoConta(string id) => _contaRepository.RetornarDado(id).GetType();
        public void RetornarConta(string id)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorPoupanca;
            decimal valorLCI;
            decimal valorLCA;
            decimal valorCDB;

            Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", "ID", "Tipo de conta", "Agencia", "Nome", "Endereço", "Saldo", "Renda Mensal", "Cheque Especial", "Poupança", "LCI", "LCA", "CDB"));
            Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", "", "", "", "", "", "", "", "", "", "", "", "", ""));

            if (conta.GetType() == typeof(ContaCorrente))
            {
                Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", conta.Id, conta.TipoConta, conta.Agencia, conta.Nome, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), conta.RetornarValorChequeEspecial().ToString("0.00"), "-", "-", "-", "-"));
            }
            if (conta.GetType() == typeof(ContaPoupanca))
            {
                Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", conta.Id, conta.TipoConta, conta.Agencia, conta.Nome, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", conta.RetornarValorPoupanca().ToString("0.00"), "-", "-", "-"));
            }
            if (conta.GetType() == typeof(ContaInvestimento))
            {
                Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{11,-15}|", conta.Id, conta.TipoConta, conta.Agencia, conta.Nome, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", "-", conta.RetornarValorLCI().ToString("0.00"), conta.RetornarValorLCA().ToString("0.00"), conta.RetornarValorCDB().ToString("0.00")));
            }
        }
        public void RetornarContas()
        {
            IList<Conta> list = _contaRepository.RetornarDados();

            decimal valorPoupanca;
            decimal valorLCI;
            decimal valorLCA;
            decimal valorCDB;

            Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", "ID", "Tipo de conta", "Agencia", "Nome", "Endereço", "Saldo", "Renda Mensal", "Cheque Especial", "Poupança", "LCI", "LCA", "CDB"));
            Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", "", "", "", "", "", "", "", "", "", "", "", "", ""));

            foreach (dynamic conta in list)
            {
                if (conta.GetType() == typeof(ContaCorrente))
                {
                    Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", conta.Id, conta.TipoConta, conta.Agencia, conta.Nome, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), conta.RetornarValorChequeEspecial().ToString("0.00"), "-", "-", "-", "-"));
                }
                if (conta.GetType() == typeof(ContaPoupanca))
                {
                    Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{9,-15}|{10,-15}|{11,-15}|", conta.Id, conta.TipoConta, conta.Agencia, conta.Nome, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", conta.RetornarValorPoupanca().ToString("0.00"), "-", "-", "-"));
                }
                if (conta.GetType() == typeof(ContaInvestimento))
                {
                    Console.WriteLine(String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-15}|{8,-15}|{11,-15}|{12,-15}|", conta.Id, conta.TipoConta, conta.Agencia, conta.Nome, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", "-", conta.RetornarValorLCI().ToString("0.00"), conta.RetornarValorLCA().ToString("0.00"), conta.RetornarValorCDB().ToString("0.00")));
                }
            }
        }
        public void RetornarSaldoConta(string id)
        {
            Console.WriteLine($"Saldo da conta {id} é: {_contaRepository.RetornarSaldo(id)}");
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
                    Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: {transacao.Valor}, Destino: {transacao.DadosContaDestino.Nome}, Data: {transacao.Data}");
                }
                if (transacao.GetType() == typeof(Investimento))
                {
                    if(transacao.DataAplicacao == null)
                    {
                        Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: {transacao.Valor}, Tipo: {transacao.TipoAplicacao}, Data Resgate: {transacao.DataResgate}");
                    }
                    else
                    {
                        Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: {transacao.Valor}, Tipo: {transacao.TipoAplicacao}, Data Aplicacão: {transacao.DataAplicacao}");
                    }
                    
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
        public void RetornarContasPorTipo(Type tipoConta)
        {
            Console.WriteLine("Todas as contas tipo:");

            IList<Conta> list = _contaRepository.RetornarContasPorTipo(tipoConta);

            foreach (Conta conta in list)
            {
                Console.WriteLine($"Conta de Id: {conta.Id}, em nome de {conta.Nome}");
            }
        }
        public void RetornarContasSaldoNegativo()
        {
            Console.WriteLine("Todas as contas com saldo negativo:");

            IList<Conta> list = _contaRepository.RetornarContasSaldoNegativo();

            foreach (Conta conta in list)
            {
                Console.WriteLine($"Conta de Id: {conta.Id}, em nome de {conta.Nome}");
            }
        }
        public void RetornarTotalInvestido()
        {
            decimal totalInvestido = _contaRepository.RetornarTotalInvestido();

            Console.WriteLine($"Total investido: {totalInvestido}");
        }
        public void AlterarDadosConta(string id, string nome, string endereco, decimal rendaMensal, AgenciaEnum agencia)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            Console.WriteLine("Dados antigos:");
            RetornarConta(id);

            _contaRepository.AlterarDados(id, nome, endereco, rendaMensal, agencia);

            if (RetornarTipoConta(id) == typeof(ContaCorrente))
            {
                conta.AlterarLimiteChequeEspecial(rendaMensal);
            }

            Console.WriteLine("Dados novos:");
            RetornarConta(id);
        }

    }
}
