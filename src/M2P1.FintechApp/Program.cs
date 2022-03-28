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

Console.WriteLine("{0}{1}{2}", "Bem vindo ao Banco Banco!", Environment.NewLine, "Escolha uma das opções:");

void MenuPrincipal()
{
    int opcao;

    Console.WriteLine("");
    Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}", "1 - Criar Conta", Environment.NewLine, "2 - Realizar Transação", Environment.NewLine, "3 - Extratos", Environment.NewLine,
    "4 - Relatórios", Environment.NewLine, "5 - Exibir Contas", Environment.NewLine, "6 - Exibir Total Investido", Environment.NewLine);

    opcao = Convert.ToInt32(Console.ReadLine());

    switch (opcao)
    {
        case 1:
            CriarConta();
            break;

        case 5:
            RetornarContas();
            break;
    }
}

void CriarConta()
{
    int opcao;

    string nome;
    string cpf;
    string endereco;
    decimal rendaMensal;

    Console.Clear();

    Console.WriteLine("");
    Console.WriteLine("{0}{1}", "Escolha uma das opções:", Environment.NewLine);
    Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}", "1 - Criar Conta Corrente", Environment.NewLine, "2 - Criar Conta Poupança", Environment.NewLine, "3 - Criar Conta Investimento", Environment.NewLine, "4 - Voltar", Environment.NewLine);

    opcao = Int32.Parse(Console.ReadLine());

    if(opcao == 4)
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

    switch (opcao)
    {
        case 1:
            _fintechApp.CriarContaCorrente(nome, cpf, endereco, rendaMensal);
            break;

        case 2:
            _fintechApp.CriarContaPoupanca(nome, cpf, endereco, rendaMensal);
            break;

        case 3:
            _fintechApp.CriarContaInvestimento(nome, cpf, endereco, rendaMensal);
            break;
    }

    MenuPrincipal();
}

void RetornarContas()
{
    _fintechApp.RetornarContas();
    MenuPrincipal();
}

MenuPrincipal();