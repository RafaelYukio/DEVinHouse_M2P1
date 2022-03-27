using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;
using M2P1.Fintech.Repositories;
using M2P1.FintechApp;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

#region Resolvendo Injeção de Dependencia

var serviceCollection = new ServiceCollection();

serviceCollection.AddScoped<IContaRepository, ContaRepository>();
serviceCollection.AddScoped<ITransferenciaRepository, TransferenciaRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var _ContaRepository = serviceProvider.GetService<IContaRepository>();
var _TransferenciaRepository = serviceProvider.GetService<ITransferenciaRepository>();

var _fintechApp = new FuncoesFintech(_ContaRepository, _TransferenciaRepository);

#endregion

_fintechApp.CriarContaPoupanca("1", "Rafael", "333", "Aqui", 1000, AgenciaEnum.Florianopolis);
_fintechApp.CriarContaPoupanca("2", "José", "444", "Ali", 2000, AgenciaEnum.Biguacu);
_fintechApp.CriarContaCorrente("3", "Ele", "555", "Lugar", 3000, AgenciaEnum.SaoJose);

Console.WriteLine("------------------------------------------");

_fintechApp.RetornarContas();

Console.WriteLine("------------------------------------------");

_fintechApp.AdicionarTransacaoConta("1", TipoTransacaoEnum.Deposito, 1000);
_fintechApp.AdicionarTransacaoConta("2", TipoTransacaoEnum.Deposito, 1000);
_fintechApp.AdicionarTransacaoConta("3", TipoTransacaoEnum.Deposito, 1000);

Console.WriteLine("------------------------------------------");

_fintechApp.RetornarContas();

Console.WriteLine("------------------------------------------");

_fintechApp.AdicionarTransacaoConta("1", TipoTransacaoEnum.Deposito, 500);
_fintechApp.AdicionarTransacaoConta("2", TipoTransacaoEnum.Saque, 200);
_fintechApp.AdicionarTransacaoConta("3", TipoTransacaoEnum.Saque, 1200);

Console.WriteLine("------------------------------------------");

_fintechApp.RetornarContas();

Console.WriteLine("------------------------------------------");

_fintechApp.AdicionarTransferencia("1", "2", 500);

Console.WriteLine("------------------------------------------");

_fintechApp.AplicarRendimento("1");
_fintechApp.RetornarContas();

Console.WriteLine("------------------------------------------");

_fintechApp.RetornarContas();
_fintechApp.RetornarTransferencias();

