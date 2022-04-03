using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;
using System.Text.RegularExpressions;

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

        public DateTime RetornarData() => _contaRepository.RetornarData();
        public void MudarDataERender(DateTime dataNova)
        {
            DateTime dataAntiga;

            dataAntiga = _contaRepository.RetornarData();

            IList<Conta> contas = _contaRepository.RetornarDados();

            foreach (dynamic conta in contas)
            {
                if (conta.GetType() == typeof(ContaPoupanca) || conta.GetType() == typeof(ContaInvestimento))
                {

                    conta.Render(DateOnly.FromDateTime(dataNova), DateOnly.FromDateTime(dataAntiga));
                }
            }

            _contaRepository.MudarData(dataNova);
            _transferenciaRepository.MudarData(dataNova);
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
        public bool VerificaCPF(string cpf)
        {
            if (Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)"))
            {
                int valorPrimeiroNumero = 0;
                int valorSegundoNumero = 0;

                bool validaPrimeiroNumero;
                bool validaSegundoNumero;

                string numerosString = new string(cpf.Where(c => Char.IsDigit(c)).ToArray());
                char[] numerosStringChars = numerosString.ToCharArray();

                int[] numeros = Array.ConvertAll(numerosStringChars, valor => (int)Char.GetNumericValue(valor));

                for (int i = 0; i < 9; i++)
                {
                    valorPrimeiroNumero += numeros[i] * (10 - i);
                }

                valorPrimeiroNumero = (valorPrimeiroNumero * 10) % 11;

                if (valorPrimeiroNumero == 10)
                {
                    valorPrimeiroNumero = 0;
                }

                if (valorPrimeiroNumero == numeros[9])
                {
                    validaPrimeiroNumero = true;
                }
                else
                {
                    validaPrimeiroNumero = false;
                }

                for (int i = 0; i < 9; i++)
                {
                    valorSegundoNumero += numeros[i] * (11 - i);
                }

                valorSegundoNumero += valorPrimeiroNumero * 2;

                valorSegundoNumero = (valorSegundoNumero * 10) % 11;

                if (valorSegundoNumero == 10)
                {
                    valorSegundoNumero = 0;
                }

                if (valorSegundoNumero == numeros[10])
                {
                    validaSegundoNumero = true;
                }
                else
                {
                    validaSegundoNumero = false;
                }

                if (validaPrimeiroNumero == true && validaSegundoNumero == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
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
                    _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Saque, "Saque", valor, _contaRepository.RetornarData()));
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
                    _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Saque, "Saque", valor, _contaRepository.RetornarData()));

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

            if (conta.GetType() == typeof(ContaCorrente))
            {
                decimal faltaPagarChequeEspecial = conta.RetornarLimiteChequeEspecial() - conta.RetornarValorChequeEspecial();

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
                _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Deposito, "Depósito", valor, _contaRepository.RetornarData()));

            }
            else
            {
                conta.Deposito(valor);
                _contaRepository.AdicionarTransacao(id, new Transacao(TipoTransacaoEnum.Deposito, "Depósito", valor, _contaRepository.RetornarData()));

                Console.WriteLine($"Depósito na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
            }

        }
        public void TransferenciaConta(string idOrigem, string idDestino, decimal valor)
        {
            DayOfWeek data = _contaRepository.RetornarData().DayOfWeek;

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

                        Transferencia transferecia = new Transferencia(TipoTransacaoEnum.Transferencia, "Transferência", valor, _contaRepository.RetornarData(), contaOrigem, contaDestino);

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
                        contaDestino.Deposito(valor);

                        Transferencia transferecia = new Transferencia(TipoTransacaoEnum.Transferencia, "Transferência", valor, _contaRepository.RetornarData(), contaOrigem, contaDestino);

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

        public void SimularRendimentoPoupanca(string id, decimal valor, DateOnly dataResgate, decimal rendimento)
        {
            dynamic conta = _contaRepository.RetornarDado(id);
            decimal valorSimulacaoRendimentoPoupanca = conta.SimulacaoRendimento(valor, dataResgate, rendimento);

            Console.WriteLine($"Valor aplicado hoje de R$ {valor}, será de R$ {valorSimulacaoRendimentoPoupanca.ToString("0.00")} em {dataResgate.ToString("dd/MM/yyyy")}");
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

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCI", TipoAplicacoEnum.Poupanca, valor, _contaRepository.RetornarData());
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

                Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate Poupança", TipoAplicacoEnum.Poupanca, valor, _contaRepository.RetornarData());
                investimento.Resgatar();

                _contaRepository.AdicionarTransacao(id, investimento);

                Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
            }

        }

        public void SimularRendimentoLCI(string id, decimal valor, DateOnly dataAplicacao, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorSimulacaoRendimentoLCI = conta.SimularRendimentoLCI(valor, dataAplicacao, dataResgate);

            Console.WriteLine($"Valor de R$ {valor} será R$ {valorSimulacaoRendimentoLCI.ToString("0.00")}, se aplicado em LCI durante {dataAplicacao.ToString("dd/MM/yyyy")} a {dataResgate.ToString("dd/MM/yyyy")}");
        }
        public void SimularRendimentoLCA(string id, decimal valor, DateOnly dataAplicacao, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorSimulacaoRendimentoLCA = conta.SimularRendimentoLCA(valor, dataAplicacao, dataResgate);

            Console.WriteLine($"Valor de R$ {valor} será R$ {valorSimulacaoRendimentoLCA.ToString("0.00")}, se aplicado em LCI durante {dataAplicacao.ToString("dd/MM/yyyy")} a {dataResgate.ToString("dd/MM/yyyy")}");
        }
        public void SimularRendimentoCDB(string id, decimal valor, DateOnly dataAplicacao, DateOnly dataResgate)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            decimal valorSimulacaoRendimentoCDB = conta.SimularRendimentoCDB(valor, dataAplicacao, dataResgate);

            Console.WriteLine($"Valor de R$ {valor} será R$ {valorSimulacaoRendimentoCDB.ToString("0.00")}, se aplicado em LCI durante {dataAplicacao.ToString("dd/MM/yyyy")} a {dataResgate.ToString("dd/MM/yyyy")}");
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

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCI", TipoAplicacoEnum.LCI, valor, _contaRepository.RetornarData());
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

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCA", TipoAplicacoEnum.LCA, valor, _contaRepository.RetornarData());
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

            Investimento investimento = new Investimento(TipoTransacaoEnum.Aplicacao, "Aplicação LCA", TipoAplicacoEnum.CDB, valor, _contaRepository.RetornarData());
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
                List<Investimento> investimentos = new List<Investimento>();
                DateTime dataPrimeiroInvestimento;

                foreach (dynamic transacao in conta.Historico)
                {
                    if (transacao.TipoAplicacao == TipoAplicacoEnum.LCI && transacao.DataAplicacao != null)
                    {
                        investimentos.Add(transacao);
                    }
                }

                dataPrimeiroInvestimento = (DateTime)investimentos.FirstOrDefault().DataAplicacao;

                if (((_contaRepository.RetornarData().Year - dataPrimeiroInvestimento.Year) * 12) + _contaRepository.RetornarData().Month - dataPrimeiroInvestimento.Month >= 6)
                {
                    conta.ResgatarLCI(valor);

                    Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate LCI", TipoAplicacoEnum.LCI, valor, _contaRepository.RetornarData());
                    investimento.Resgatar();

                    _contaRepository.AdicionarTransacao(id, investimento);

                    Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
                }
                else
                {
                    Console.WriteLine($"Resgate não realizado. Tempo mínimo de 6 meses para LCI!");
                }
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
                List<Investimento> investimentos = new List<Investimento>();
                DateTime dataPrimeiroInvestimento;

                foreach (dynamic transacao in conta.Historico)
                {
                    if (transacao.TipoAplicacao == TipoAplicacoEnum.LCA && transacao.DataAplicacao != null)
                    {
                        investimentos.Add(transacao);
                    }
                }

                dataPrimeiroInvestimento = (DateTime)investimentos.FirstOrDefault().DataAplicacao;

                if (((_contaRepository.RetornarData().Year - dataPrimeiroInvestimento.Year) * 12) + _contaRepository.RetornarData().Month - dataPrimeiroInvestimento.Month >= 12)
                {
                    conta.ResgatarLCA(valor);

                    Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate LCA", TipoAplicacoEnum.LCA, valor, _contaRepository.RetornarData());
                    investimento.Resgatar();

                    _contaRepository.AdicionarTransacao(id, investimento);

                    Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
                }
                else
                {
                    Console.WriteLine($"Resgate não realizado. Tempo mínimo de 12 meses para LCA!");
                }
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
                List<Investimento> investimentos = new List<Investimento>();
                DateTime dataPrimeiroInvestimento;

                foreach (dynamic transacao in conta.Historico)
                {
                    if (transacao.TipoAplicacao == TipoAplicacoEnum.CDB && transacao.DataAplicacao != null)
                    {
                        investimentos.Add(transacao);
                    }
                }

                dataPrimeiroInvestimento = (DateTime)investimentos.FirstOrDefault().DataAplicacao;

                if (((_contaRepository.RetornarData().Year - dataPrimeiroInvestimento.Year) * 12) + _contaRepository.RetornarData().Month - dataPrimeiroInvestimento.Month >= 36)
                {
                    conta.ResgatarCDB(valor);

                    Investimento investimento = new Investimento(TipoTransacaoEnum.Resgate, "Resgate CDB", TipoAplicacoEnum.CDB, valor, _contaRepository.RetornarData());
                    investimento.Resgatar();

                    _contaRepository.AdicionarTransacao(id, investimento);

                    Console.WriteLine($"Resgate na conta número/ID {id} de {conta.Nome}, no valor de R$ {valor}, realizada com sucesso!");
                }
                else
                {
                    Console.WriteLine($"Resgate não realizado. Tempo mínimo de 36 meses para CDB!");
                }
            }
        }

        public void TextoRetornarContas(IList<Conta> list)
        {
            string agencia = "";

            Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", "ID", "Tipo de conta", "Agencia", "Nome", "CPF", "Endereço", "Saldo", "Renda Mensal", "Cheque Especial", "Poupança", "LCI", "LCA", "CDB"));
            Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", "", "", "", "", "", "", "", "", "", "", "", "", "", ""));

            foreach (dynamic conta in list)
            {
                switch (conta.Agencia)
                {
                    case AgenciaEnum.Florianopolis:
                        agencia = "Florianópolis";
                        break;
                    case AgenciaEnum.Biguacu:
                        agencia = "Biguaçu";
                        break;
                    case AgenciaEnum.SaoJose:
                        agencia = "São José";
                        break;
                }

                if (conta.GetType() == typeof(ContaCorrente))
                {
                    Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", conta.Id, conta.TipoConta, agencia, conta.Nome, conta.CPF, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), conta.RetornarValorChequeEspecial().ToString("0.00"), "-", "-", "-", "-"));
                }
                if (conta.GetType() == typeof(ContaPoupanca))
                {
                    Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", conta.Id, conta.TipoConta, agencia, conta.Nome, conta.CPF, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", conta.RetornarValorPoupanca().ToString("0.00"), "-", "-", "-"));
                }
                if (conta.GetType() == typeof(ContaInvestimento))
                {
                    Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", conta.Id, conta.TipoConta, agencia, conta.Nome, conta.CPF, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", "-", conta.RetornarValorLCI().ToString("0.00"), conta.RetornarValorLCA().ToString("0.00"), conta.RetornarValorCDB().ToString("0.00")));
                }
            }
        }

        public Type RetornarTipoConta(string id) => _contaRepository.RetornarDado(id).GetType();
        public void RetornarConta(string id)
        {
            dynamic conta = _contaRepository.RetornarDado(id);

            string agencia = "";
            decimal valorPoupanca;
            decimal valorLCI;
            decimal valorLCA;
            decimal valorCDB;

            switch (conta.Agencia)
            {
                case AgenciaEnum.Florianopolis:
                    agencia = "Florianópolis";
                    break;
                case AgenciaEnum.Biguacu:
                    agencia = "Biguaçu";
                    break;
                case AgenciaEnum.SaoJose:
                    agencia = "São José";
                    break;
            }

            Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", "ID", "Tipo de conta", "Agencia", "Nome", "CPF", "Endereço", "Saldo", "Renda Mensal", "Cheque Especial", "Poupança", "LCI", "LCA", "CDB"));
            Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", "", "", "", "", "", "", "", "", "", "", "", "", "", ""));

            if (conta.GetType() == typeof(ContaCorrente))
            {
                Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", conta.Id, conta.TipoConta, agencia, conta.Nome, conta.CPF, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), conta.RetornarValorChequeEspecial().ToString("0.00"), "-", "-", "-", "-"));
            }
            if (conta.GetType() == typeof(ContaPoupanca))
            {
                Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", conta.Id, conta.TipoConta, agencia, conta.Nome, conta.CPF, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", conta.RetornarValorPoupanca().ToString("0.00"), "-", "-", "-"));
            }
            if (conta.GetType() == typeof(ContaInvestimento))
            {
                Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|{7,-13}|{8,-15}|{9,-13}|{10,-13}|{11,-13}|{12,-13}|", conta.Id, conta.TipoConta, agencia, conta.Nome, conta.CPF, conta.Endereco, conta.Saldo().ToString("0.00"), conta.RendaMensal.ToString("0.00"), "-", "-", conta.RetornarValorLCI().ToString("0.00"), conta.RetornarValorLCA().ToString("0.00"), conta.RetornarValorCDB().ToString("0.00")));
            }
        }
        public void RetornarContas()
        {
            IList<Conta> list = _contaRepository.RetornarDados();

            decimal valorPoupanca;
            decimal valorLCI;
            decimal valorLCA;
            decimal valorCDB;

            TextoRetornarContas(list);
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
                    Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: R$ {transacao.Valor}, Data: {transacao.Data.ToString("dd/MM/yyyy")}");
                }
                if (transacao.GetType() == typeof(Transferencia))
                {
                    Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: R$ {transacao.Valor}, Destino: {transacao.DadosContaDestino.Nome}, Data: {transacao.Data.ToString("dd/MM/yyyy")}");
                }
                if (transacao.GetType() == typeof(Investimento))
                {
                    if (transacao.DataAplicacao == null)
                    {
                        Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: R$ {transacao.Valor}, Tipo: {transacao.TipoAplicacao}, Data Resgate: {transacao.DataResgate.ToString("dd/MM/yyyy")}");
                    }
                    else
                    {
                        Console.WriteLine($"Tipo: {transacao.TipoTransacao}, Descrição: {transacao.Descricao}, Valor: R$ {transacao.Valor}, Tipo: {transacao.TipoAplicacao}, Data Aplicacão: {transacao.DataAplicacao.ToString("dd/MM/yyyy")}");
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
                Console.WriteLine($"Origem: {transferencia.DadosContaOrigem.Nome}, destino: {transferencia.DadosContaDestino.Nome}, valor: R$ {transferencia.Valor}, data: {transferencia.Data.ToString("dd/MM/yyyy")}");
            }
        }
        public void RetornarContasPorTipo(Type tipoConta)
        {
            IList<Conta> list = _contaRepository.RetornarContasPorTipo(tipoConta);

            TextoRetornarContas(list);
        }
        public void RetornarContasSaldoNegativo()
        {
            IList<Conta> list = _contaRepository.RetornarContasSaldoNegativo();

            TextoRetornarContas(list);
        }
        public void RetornarTotalInvestido()
        {
            decimal totalInvestido = _contaRepository.RetornarTotalInvestido();

            Console.WriteLine($"Total investido no banco: R$ {totalInvestido.ToString("0.00")}");
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
