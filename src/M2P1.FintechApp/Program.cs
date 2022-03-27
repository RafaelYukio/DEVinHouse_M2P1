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
_fintechApp.CriarContaInvestimento("2", "José", "444", "Ali", 2000, AgenciaEnum.Biguacu);
_fintechApp.CriarContaCorrente("3", "Ele", "555", "Lugar", 3000, AgenciaEnum.SaoJose);

Console.WriteLine("------------------------------------------");

_fintechApp.RetornarContas();

Console.WriteLine("------------------------------------------");

_fintechApp.AplicarPoupanca("1", 1000);
_fintechApp.AplicarLCI("2", 1000);
_fintechApp.AplicarLCA("2", 2000);
_fintechApp.AplicarCDB("2", 3000);
_fintechApp.DepositoConta("3", 5000);

Console.WriteLine("------------------------------------------");

_fintechApp.RetornarContas();

Console.WriteLine("------------------------------------------");

_fintechApp.SimularRendimentoPoupanca("1", 1000, DateOnly.FromDateTime(DateTime.Now.AddDays(300)), 1.005M);

_fintechApp.SimularRendimentoLCI("2", 1000, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(731)));