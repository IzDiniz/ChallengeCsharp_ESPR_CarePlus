# Gerenciador de Atividades de Saúde

Este é um aplicativo de console em C# projetado para registrar e acompanhar atividades de saúde, como minutos de exercício, litros de água ingeridos ou horas de sono. Os dados são armazenados em memória (arrays internos) enquanto o programa está em execução.

## Funcionalidades

O programa oferece um menu simples com as seguintes opções:

1.  **Adicionar registro**: Permite ao usuário informar o tipo de atividade, a data (usa a data atual para simplificação) e o valor (número de minutos, litros, etc.).
2.  **Listar registros**: Exibe todos os registros cadastrados de forma organizada.
3.  **Exibir estatísticas**: Calcula e apresenta a soma total e a média dos valores informados para cada tipo de atividade.
4.  **Sair**: Encerra o programa.

## Requisitos Técnicos Atendidos

*   **Estrutura em Métodos/Funções**: O código é organizado em métodos estáticos (`ExibirMenu`, `AdicionarRegistro`, `ListarRegistros`, `ExibirEstatisticas`) para cada operação, conforme solicitado.
*   **Armazenamento Interno**: Os dados são armazenados em uma `List<RegistroAtividade>` estática, que atende ao requisito de usar "arrays internos" (coleções em memória).
*   **Validação de Entrada**: A função `AdicionarRegistro` valida se o valor inserido é um número e se é positivo (maior que zero), tratando erros simples.
*   **Interface de Texto Clara**: A interface utiliza mensagens diretas, opções numeradas e um retorno amigável ao usuário.
*   **Comentários no Código**: O arquivo `Program.cs` contém comentários explicativos.

## Como Executar

Para executar esta aplicação, você precisa ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado em sua máquina.

1.  **Navegue até o diretório do projeto:**
    \`\`\`bash
    cd GerenciadorSaude
    \`\`\`

2.  **Execute a aplicação:**
    \`\`\`bash
    dotnet run
    \`\`\`

O programa será iniciado e o menu principal será exibido no console.
