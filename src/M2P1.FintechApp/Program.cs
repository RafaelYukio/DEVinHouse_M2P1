using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;
using M2P1.Fintech.Repositories;
using M2P1.FintechApp;
using Microsoft.Extensions.DependencyInjection;

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

bool VerificaConta(string contaNumero) => _fintechApp.VerificaContaNumero(contaNumero);

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
            Environment.NewLine, "4 - Saldo", Environment.NewLine, "5 - Extrato", Environment.NewLine, "4 - Informações da conta", Environment.NewLine, "5 - Alterar dados", Environment.NewLine, "6 - Voltar menu principal", Environment.NewLine);

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
            }
        }
        if (_fintechApp.RetornarTipoConta(contaNumero) == typeof(ContaPoupanca))
        {

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

void RetornarContas()
{
    _fintechApp.RetornarContas();
    MenuPrincipal();
}

MenuPrincipal();