using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;
using M2P1.Fintech.Repositories;
using M2P1.FintechApp;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

#region Resolvendo Injeção de Dependencia

var serviceCollection = new ServiceCollection();

serviceCollection.AddScoped<IContaRepository, ContaRepository>();
serviceCollection.AddScoped<ITransferenciaRepository, TransferenciaRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var _ContaRepository = serviceProvider.GetService<IContaRepository>();
var _TransferenciaRepository = serviceProvider.GetService<ITransferenciaRepository>();

var _fintechApp = new FuncoesFintech(_ContaRepository, _TransferenciaRepository);

#endregion

Console.WriteLine("Bem vindo ao Banco Banco!");

bool VerificaConta(string contaNumero) => _fintechApp.VerificaContaNumero(contaNumero);


void MenuPrincipal()
{
    string stringOpcao;
    int opcao;

    Console.WriteLine("");
    Console.WriteLine("{0}{1}", "Escolha uma das opções:", Environment.NewLine);
    Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}", "1 - Criar Conta", Environment.NewLine, "2 - Acessar conta", Environment.NewLine,
    "3 - Relatórios", Environment.NewLine, "4 - Exibir Contas", Environment.NewLine, "5 - Exibir Total Investido", Environment.NewLine
    , "6 - Alterar data", Environment.NewLine);

    stringOpcao = Console.ReadLine();

    if (Int32.TryParse(stringOpcao, out opcao) && opcao > 0 && opcao < 7)
    {
        switch (opcao)
        {
            case 1:
                Console.Clear();
                CriarConta();
                break;

            case 2:
                Console.Clear();
                AcessarConta();
                break;

            case 4:
                Console.Clear();
                RetornarContas();
                break;
        }
    }
    else
    {
        Console.WriteLine("Escolha uma opção válida!");
        MenuPrincipal();
    }
}

void CriarConta()
{
    string opcao;

    string nome;
    string cpf;
    string endereco;
    decimal rendaMensal;

    Console.WriteLine("");
    Console.WriteLine("{0}{1}", "Escolha uma das opções:", Environment.NewLine);
    Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}", "1 - Criar Conta Corrente", Environment.NewLine, "2 - Criar Conta Poupança", Environment.NewLine, "3 - Criar Conta Investimento", Environment.NewLine, "4 - Voltar", Environment.NewLine);

    opcao = Console.ReadLine();

    if (opcao == "4")
    {
        MenuPrincipal();
    }

    Console.WriteLine("");
    Console.WriteLine("Digite seu nome:");
    nome = Console.ReadLine();
    Console.WriteLine("Digite seu CPF:");
    cpf = Console.ReadLine();
    Console.WriteLine("Digite seu endereço:");
    endereco = Console.ReadLine();
    Console.WriteLine("Digite sua renda mensal:");
    rendaMensal = Decimal.Parse(Console.ReadLine());

    Console.Clear();

    switch (opcao)
    {
        case "1":
            _fintechApp.CriarContaCorrente(nome, cpf, endereco, rendaMensal);
            break;

        case "2":
            _fintechApp.CriarContaPoupanca(nome, cpf, endereco, rendaMensal);
            break;

        case "3":
            _fintechApp.CriarContaInvestimento(nome, cpf, endereco, rendaMensal);
            break;
    }

    MenuPrincipal();
}

void AcessarConta()
{
    string contaNumero;

    Console.WriteLine("");
    Console.WriteLine("Digite o número da sua conta:");
    contaNumero = Console.ReadLine();

    if (VerificaConta(contaNumero))
    {
        string opcao;

        Console.WriteLine("{0}{1}", "Escolha uma das opções:", Environment.NewLine);

        if (_fintechApp.RetornarTipoConta(contaNumero) == typeof(ContaCorrente))
        {
            Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}", "1 - Saque", Environment.NewLine, "2 - Depósito", Environment.NewLine, "3 - Transferência",
            Environment.NewLine, "4 - Saldo", Environment.NewLine, "5 - Extrato", Environment.NewLine, "6 - Informações da conta", Environment.NewLine, "7 - Alterar dados", Environment.NewLine, "8 - Voltar ao menu principal", Environment.NewLine);

            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    Saque(contaNumero.ToString());
                    break;
                case "2":
                    Console.Clear();
                    Deposito(contaNumero.ToString());
                    break;
                case "3":
                    Console.Clear();
                    Transferencia(contaNumero.ToString());
                    break;
                case "4":
                    Console.Clear();
                    SaldoConta(contaNumero.ToString());
                    break;
                case "5":
                    Console.Clear();
                    RetornarExtrato(contaNumero.ToString());
                    break;
                case "6":
                    Console.Clear();
                    RetornarConta(contaNumero.ToString());
                    break;
                case "7":
                    Console.Clear();
                    AlterarDados(contaNumero.ToString());
                    break;
                case "8":
                    Console.Clear();
                    MenuPrincipal();
                    break;
            }
        }
        if (_fintechApp.RetornarTipoConta(contaNumero) == typeof(ContaPoupanca))
        {
            Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}", "1 - Saque", Environment.NewLine, "2 - Depósito", Environment.NewLine, "3 - Aplicar poupança", Environment.NewLine, "4 - Resgatar poupança", Environment.NewLine, "5 - Simular poupança", Environment.NewLine, "6 - Transferência",
            Environment.NewLine, "7 - Saldo", Environment.NewLine, "8 - Extrato", Environment.NewLine, "9 - Informações da conta", Environment.NewLine, "10 - Alterar dados", Environment.NewLine, "11 - Voltar ao menu principal", Environment.NewLine);

            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    Saque(contaNumero.ToString());
                    break;
                case "2":
                    Console.Clear();
                    Deposito(contaNumero.ToString());
                    break;
                case "3":
                    Console.Clear();
                    AplicarPoupanca(contaNumero.ToString());
                    break;
                case "4":
                    Console.Clear();
                    ResgatarPoupanca(contaNumero.ToString());
                    break;
                case "5":
                    Console.Clear();
                    SimularPoupanca(contaNumero.ToString());
                    break;
                case "6":
                    Console.Clear();
                    Transferencia(contaNumero.ToString());
                    break;
                case "7":
                    Console.Clear();
                    SaldoConta(contaNumero.ToString());
                    break;
                case "8":
                    Console.Clear();
                    RetornarExtrato(contaNumero.ToString());
                    break;
                case "9":
                    Console.Clear();
                    RetornarConta(contaNumero.ToString());
                    break;
                case "10":
                    Console.Clear();
                    AlterarDados(contaNumero.ToString());
                    break;
                case "11":
                    Console.Clear();
                    MenuPrincipal();
                    break;
            }
        }

    }
    else
    {
        Console.WriteLine("Conta número inexistente!");
        MenuPrincipal();
    }

}


void Saque(string id)
{
    decimal valor;

    Console.WriteLine("Digite o valor do saque:");

    if (Decimal.TryParse(Console.ReadLine(), out valor))
    {
        _fintechApp.SaqueConta(id, valor);
        MenuPrincipal();
    }
    else
    {
        Console.WriteLine("Valor inválido!");
        MenuPrincipal();
    }
}

void Deposito(string id)
{
    decimal valor;

    Console.WriteLine("Digite o valor do depósito:");
    if (Decimal.TryParse(Console.ReadLine(), out valor))
    {
        _fintechApp.DepositoConta(id, valor);
        MenuPrincipal();
    }
    else
    {
        Console.WriteLine("Valor inválido!");
        MenuPrincipal();
    }
}

void AplicarPoupanca(string id)
{
    decimal valor;

    Console.WriteLine("Digite o valor do depósito:");
    if (Decimal.TryParse(Console.ReadLine(), out valor))
    {
        _fintechApp.AplicarPoupanca(id, valor);
        MenuPrincipal();
    }
    else
    {
        Console.WriteLine("Valor inválido!");
        MenuPrincipal();
    }
}

void ResgatarPoupanca(string id)
{
    decimal valor;

    Console.WriteLine("Digite o valor do resgate:");
    if (Decimal.TryParse(Console.ReadLine(), out valor))
    {
        _fintechApp.ResgatarPoupanca(id, valor);
        MenuPrincipal();
    }
    else
    {
        Console.WriteLine("Valor inválido!");
        MenuPrincipal();
    }
}

void SimularPoupanca(string id)
{
    decimal valor;
    DateOnly dataResgate;
    double rendimento;

    Console.WriteLine("Digite o valor do depósito:");
    if (Decimal.TryParse(Console.ReadLine(), out valor))
    {
        Console.WriteLine("Digite a data de resgate (dd/mm/aaaa):");
        if (DateOnly.TryParseExact(Console.ReadLine(),"dd/MM/yyyy", CultureInfo.InvariantCulture,  DateTimeStyles.None,out dataResgate))
        {
            Console.WriteLine("Digite o rendimento anual da poupança (%):");
            if (double.TryParse(Console.ReadLine(), out rendimento))
            {
                _fintechApp.SimularRendimentoPoupanca(id, valor, dataResgate, (decimal)Math.Pow(rendimento / 100 + 1, 1.0 / 12));
            }
        }
    }
    else
    {
        Console.WriteLine("Valor inválido!");
    }
    MenuPrincipal();
}

void Transferencia(string id)
{
    decimal valor;
    string idDestino;

    Console.WriteLine("Digite o número da conta destino:");
    idDestino = Console.ReadLine();

    if (VerificaConta(idDestino))
    {
        Console.WriteLine("Digite o valor da transferência:");
        if (Decimal.TryParse(Console.ReadLine(), out valor))
        {
            _fintechApp.TransferenciaConta(id, idDestino, valor);
            MenuPrincipal();
        }
        else
        {
            Console.WriteLine("Valor inválido!");
            MenuPrincipal();
        }
    }
    else
    {
        Console.WriteLine("Conta número inexistente!");
        MenuPrincipal();
    }
}


void SaldoConta(string id)
{
    _fintechApp.RetornarSaldoConta(id);
    MenuPrincipal();
}


void RetornarConta(string id)
{
    _fintechApp.RetornarConta(id);
    MenuPrincipal();
}

void RetornarContas()
{
    _fintechApp.RetornarContas();
    MenuPrincipal();
}

void RetornarExtrato(string id)
{
    _fintechApp.RetornarTransacoesConta(id);
    MenuPrincipal();
}


void AlterarDados(string id)
{
    if (VerificaConta(id))
    {
        string nome;
        string endereco;
        decimal rendaMensal;
        AgenciaEnum agencia = AgenciaEnum.Florianopolis;

        Console.WriteLine("Digite novo nome:");
        nome = Console.ReadLine();
        Console.WriteLine("Digite novo endereço:");
        endereco = Console.ReadLine();
        Console.WriteLine("Digite nova renda mensal:");
        if (Decimal.TryParse(Console.ReadLine(), out rendaMensal))
        {
            Console.WriteLine("Escolha nova agência:");
            Console.WriteLine("");
            Console.WriteLine("1 - Florianópolis");
            Console.WriteLine("2 - São José");
            Console.WriteLine("3 - Biguaçu");

            switch (Console.ReadLine())
            {
                case "1":
                    agencia = AgenciaEnum.Florianopolis;
                    break;
                case "2":
                    agencia = AgenciaEnum.SaoJose;
                    break;
                case "3":
                    agencia = AgenciaEnum.Biguacu;
                    break;
            }

            _fintechApp.AlterarDadosConta(id, nome, endereco, rendaMensal, agencia);
            MenuPrincipal();
        }
        else
        {
            Console.WriteLine("Valor inválido!");
            MenuPrincipal();
        }
    }
    else
    {
        Console.WriteLine("Conta número inexistente!");
        MenuPrincipal();
    }
}


MenuPrincipal();