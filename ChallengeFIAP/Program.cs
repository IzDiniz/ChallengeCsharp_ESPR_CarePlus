using System;
using System.Collections.Generic;
using System.Linq;

// Estrutura para armazenar um registro de atividade
public class RegistroAtividade
{
    public string TipoAtividade { get; set; }
    public DateTime Data { get; set; }
    public double Valor { get; set; } // Ex: minutos de exercício, litros de água, horas de sono
}

public class GerenciadorAtividades
{
    // Array interno para armazenar os registros. Usamos List<T> para flexibilidade,
    // mas o conceito é o de uma coleção interna, como solicitado.
    private static List<RegistroAtividade> registros = new List<RegistroAtividade>();

    public static void Main(string[] args)
    {
        Console.WriteLine("=== Gerenciador de Atividades de Saúde ===");

        bool continuar = true;
        while (continuar)
        {
            ExibirMenu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AdicionarRegistro();
                    break;
                case "2":
                    ListarRegistros();
                    break;
                case "3":
                    ExibirEstatisticas();
                    break;
                case "4":
                    EditarRegistro();
                    break;
                case "5":
                    RemoverRegistro();
                    break;
                case "6":
                    continuar = false;
                    Console.WriteLine("Saindo do programa. Até mais!");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção de 1 a 6.");
                    break;
            }

            if (continuar)
            {
                Console.WriteLine("\n--- Fim da Operação ---\n");
            }
        }
    }

    // Método para exibir o menu principal
    private static void ExibirMenu()
    {
        Console.WriteLine("\n--- Menu Principal ---");
        Console.WriteLine("1. Adicionar registro");
        Console.WriteLine("2. Listar registros");
        Console.WriteLine("3. Exibir estatísticas");
        Console.WriteLine("4. Editar registro");
        Console.WriteLine("5. Remover registro");
        Console.WriteLine("6. Sair");
        Console.Write("Escolha uma opção: ");
    }

    // Método para adicionar um novo registro 
    private static void AdicionarRegistro()
    {
        Console.WriteLine("\n--- Adicionar Novo Registro ---");

        // 1. Tipo de Atividade
        Console.Write("Informe o tipo de atividade (Ex: Caminhada, Água, Sono): ");
        string tipo = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(tipo))
        {
            Console.WriteLine("Erro: O tipo de atividade não pode ser vazio.");
            return;
        }

        // 2. Data (Simplificação: usa a data atual, mas pode ser expandido)
        DateTime data = DateTime.Now.Date;
        Console.WriteLine($"Data do registro: {data.ToShortDateString()} (Usando data atual)");

        // 3. Valor com Validação (Requisito de Validação)
        double valor;
        Console.Write("Informe o valor (Ex: minutos, litros, horas): ");
        string inputValor = Console.ReadLine();

        if (!double.TryParse(inputValor, out valor))
        {
            Console.WriteLine("Erro: O valor informado não é um número válido.");
            return;
        }

        if (valor <= 0) // Validação: valor não pode ser negativo ou zero
        {
            Console.WriteLine("Erro: O valor deve ser um número positivo.");
            return;
        }

        // Cria e adiciona o novo registro
        RegistroAtividade novoRegistro = new RegistroAtividade
        {
            TipoAtividade = tipo.Trim(),
            Data = data,
            Valor = valor
        };

        registros.Add(novoRegistro);
        Console.WriteLine($"\nSucesso! Registro de '{novoRegistro.TipoAtividade}' com valor {novoRegistro.Valor} adicionado.");
    }

    // Método para listar todos os registros 
    // O parâmetro 'comIndice' é usado para exibir um índice para edição/remoção
    private static void ListarRegistros(bool comIndice = false)
    {
        Console.WriteLine("\n--- Registros Cadastrados ---");

        if (registros.Count == 0)
        {
            Console.WriteLine("Nenhum registro cadastrado ainda.");
            return;
        }

        // Exibe os registros em formato de tabela
        string formato = comIndice ? "{0,-5} | {1,-20} | {2,-10} | {3,-10}" : "{0,-20} | {1,-10} | {2,-10}";
        string cabecalho = comIndice ? "ÍNDICE | ATIVIDADE | DATA | VALOR" : "ATIVIDADE | DATA | VALOR";
        string separador = comIndice ? new string('-', 51) : new string('-', 43);

        Console.WriteLine(cabecalho);
        Console.WriteLine(separador);

        for (int i = 0; i < registros.Count; i++)
        {
            var reg = registros[i];
            if (comIndice)
            {
                Console.WriteLine(formato,
                                  i + 1, // Índice 1-based
                                  reg.TipoAtividade,
                                  reg.Data.ToShortDateString(),
                                  reg.Valor);
            }
            else
            {
                Console.WriteLine(formato,
                                  reg.TipoAtividade,
                                  reg.Data.ToShortDateString(),
                                  reg.Valor);
            }
        }
    }

    // Método auxiliar para obter um índice de registro válido
    private static int ObterIndiceRegistro(string acao)
    {
        ListarRegistros(true);
        Console.Write($"\nInforme o ÍNDICE do registro que deseja {acao} (ou 0 para cancelar): ");

        if (!int.TryParse(Console.ReadLine(), out int indice) || indice < 0 || indice > registros.Count)
        {
            Console.WriteLine("Erro: Índice inválido.");
            return -1;
        }

        return indice - 1; // Retorna índice 0-based
    }

    // Método para editar um registro existente
    private static void EditarRegistro()
    {
        Console.WriteLine("\n--- Editar Registro ---");
        if (registros.Count == 0)
        {
            Console.WriteLine("Nenhum registro para editar.");
            return;
        }

        int indice = ObterIndiceRegistro("editar");
        if (indice < 0) return;
        if (indice == -1) return; // Cancelado ou inválido

        RegistroAtividade registro = registros[indice];
        Console.WriteLine($"\nRegistro selecionado: {registro.TipoAtividade} - {registro.Valor}");

        // 1. Novo Tipo de Atividade
        Console.Write($"Novo tipo de atividade (Atual: {registro.TipoAtividade}. Deixe em branco para manter): ");
        string novoTipo = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(novoTipo))
        {
            registro.TipoAtividade = novoTipo.Trim();
        }

        // 2. Novo Valor com Validação
        Console.Write($"Novo valor (Atual: {registro.Valor}. Deixe em branco para manter): ");
        string inputNovoValor = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(inputNovoValor))
        {
            if (!double.TryParse(inputNovoValor, out double novoValor))
            {
                Console.WriteLine("Erro: O valor informado não é um número válido. Edição cancelada.");
                return;
            }

            if (novoValor <= 0)
            {
                Console.WriteLine("Erro: O valor deve ser um número positivo. Edição cancelada.");
                return;
            }

            registro.Valor = novoValor;
        }

        // A data não é editável para simplificação
        Console.WriteLine("\nSucesso! Registro editado:");
        Console.WriteLine($"Tipo: {registro.TipoAtividade}, Valor: {registro.Valor}");
    }

    // Método para remover um registro existente
    private static void RemoverRegistro()
    {
        Console.WriteLine("\n--- Remover Registro ---");
        if (registros.Count == 0)
        {
            Console.WriteLine("Nenhum registro para remover.");
            return;
        }

        int indice = ObterIndiceRegistro("remover");
        if (indice < 0) return;
        if (indice == -1) return; // Cancelado ou inválido

        RegistroAtividade registroRemovido = registros[indice];

        Console.Write($"Tem certeza que deseja remover o registro: {registroRemovido.TipoAtividade} - {registroRemovido.Valor}? (S/N): ");
        string confirmacao = Console.ReadLine().Trim().ToUpper();

        if (confirmacao == "S")
        {
            registros.RemoveAt(indice);
            Console.WriteLine($"\nSucesso! Registro de '{registroRemovido.TipoAtividade}' removido.");
        }
        else
        {
            Console.WriteLine("\nRemoção cancelada.");
        }
    }

    // Método para exibir estatísticas
    private static void ExibirEstatisticas()
    {
        Console.WriteLine("\n--- Estatísticas por Tipo de Atividade ---");

        if (registros.Count == 0)
        {
            Console.WriteLine("Nenhum registro para calcular estatísticas.");
            return;
        }

        // Agrupa os registros por TipoAtividade e calcula as estatísticas
        var estatisticas = registros
            .GroupBy(r => r.TipoAtividade)
            .Select(g => new
            {
                Tipo = g.Key,
                Total = g.Sum(r => r.Valor),
                Media = g.Average(r => r.Valor),
                Contagem = g.Count()
            })
            .OrderBy(e => e.Tipo);        // Exibe as estatísticas em formato de tabela
        Console.WriteLine("{0,-20} | {1,-10} | {2,-10} | {3,-10}", "ATIVIDADE", "TOTAL", "MÉDIA", "CONTAGEM");
        Console.WriteLine(new string('-', 57));

        foreach (var stats in estatisticas)
        {
            Console.WriteLine("{0,-20} | {1,-10:F2} | {2,-10:F2} | {3,-10}",
                              stats.Tipo,
                              stats.Total,
                              stats.Media,
                              stats.Contagem);
        }
    }
}
