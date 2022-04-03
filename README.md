# DEVinHouse_M2P1
Projeto 1 - Módulo 2 do curso DEVinHouse (SENAI) 
Última atualização: 03/04/2022

## Objetivos

Criação de um sistema para _fintech_.

<details>
  <summary>Requisitos</summary>
  
- Sistema inteiro em C#;
- Seguir o roteiro proposto;
- Entrada de dados pelo usuário e saída das mensagens;
- Interação do usuário via console;
- Utilização apenas do console.
</details>

<details>
  <summary>Critérios para máxima avaliação</summary>
  
- Algoritmo organizado;
- Menu de fácil entendimento;
- Utilização correta de herança;
- Tratamento de erros (utilizando estruturas específicas e reutilizáveis);
- Implementação de classes com atributos e métodos;
- Implementação dos relatório propostos;
- Implementação das operações bancárias e transações propostas.
</details>
<br/>

## Etapas

- C#
    - Criação das contas tipo:
        - Corrente;
            - Possibilidade de cheque especial igual a 10% da renda mensal, podendo ficar com saldo negativo neste valor;
            - Extrato das transações.
        - Poupança;
            - Simulação de rendimento na poupança por determinado tempo;
            - Extrato das transações.
        - Investimento;
            - Possibilidades de investimento (com rendimentos):
                - LCI (8% ao ano);
                - LCA (9% ao ano);
                - CDB (10% ao ano).
            - Simulação de rendimento com entradas:
                - Valor;
                - Tempo;
                - Perguntar se deseja realizar o investimento.
            - Extrato das transações;
                - Valor aplicado;
                - Tipo de aplicação;
                - Data da aplicação;
                - Data da retirada.
            - Rendimento aplicado diário.
    - Atributos e métodos das contas:
        - Conta:
            - Atributos:
                - Nome;
                - CPF (validar CPF);
                - Endereço;
                - Renda Mensal;
                - Conta número (geração automática);
                - Agência (escolha automática e apresentar qual a escolhida):
                    - 001 - Florianópoilis;
                    - 002 - Biguaçu;
                    - 003 - São José.
                -Saldo.
            - Métodos:
                - Saque;
                - Depósito;
                - Saldo;
                - Extrato;
                - Transferência;
                - Alterar dados cadastrais (exceto CPF).
    - Histórico de transferências;
        - Dados da conta origem;
        - Dados da conta destino;
        - Valor;
        - Data (utilizar data do sistema).
    - Relatórios:
        - Listar todas as contas por:
            - Correntes;
            - Poupanças;
            - Investimentos.
        - Contas com saldo negativo;
        - Total do valor investido;
        - Todas as transações de uma determinada conta.
    - Validações (não permitir):
        - Transferências cujo valor ultrapasse o saldo (mais o cheque especial, no caso da conta corrente);
        - Operações em datas anteriores;
        - Transferências nos finais de semana;
        - Transferências para si próprio.
    - Sistema deve inicializar com a data do computador;
    - Função de mudar data do sistema e render aplicações.