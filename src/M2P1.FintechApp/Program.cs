using M2P1.Fintech.Entidades;
using M2P1.Fintech.Enums;

Console.WriteLine("Hello, World!");

ContaPoupanca novaConta = new ContaPoupanca("1", "Rafael", "111111", "Aqui", 1000, AgenciaEnum.Biguacu);

Console.WriteLine(novaConta.ValorSaldo);

novaConta.Deposito(1000);

Console.WriteLine(novaConta.ValorSaldo);

novaConta.Saque(500);

Console.WriteLine(novaConta.ValorSaldo);