using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;
using M2P1.Fintech.Interfaces;
using M2P1.Fintech.Repositories;
using M2P1.FintechApp;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

#region Resolvendo Injeção de Dependencia

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<IContaPoupancaRepository, ContaPoupancaRepository>();
var serviceProvider = serviceCollection.BuildServiceProvider();
var _acaoContaRepository = serviceProvider.GetService<IContaPoupancaRepository>();
var _fintechApp = new FuncoesFintech(_acaoContaRepository);

#endregion

_fintechApp.CriarContaPoupanca("1", "Rafael", "333", "Aqui", 1000, AgenciaEnum.Florianopolis);
_fintechApp.CriarContaPoupanca("2", "José", "444", "Ali", 2000, AgenciaEnum.Biguacu);

_fintechApp.RetornarContas();