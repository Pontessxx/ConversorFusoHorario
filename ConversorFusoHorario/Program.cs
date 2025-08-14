using System;
using System.Collections.Generic;
using ConversorFusoHorario;

class Program
{
    static void Main()
    {
        var conversor = new ConversorHora();
        var agenda = new List<IAgendaEntrada>();

        var fusosDisponiveis = new List<string>
        {
            "E. South America Standard Time", // Brasil
            "Pacific Standard Time",          // Califórnia
            "GMT Standard Time"               // Reino Unido
        };

        Console.WriteLine("*** Cadastro de Compromissos ***");

        while (true)
        {
            Console.Write("Digite a data e hora do compromisso (ex: 2025-08-14 14:30): ");
            string dataHoraStr = Console.ReadLine() ?? "";
            if (!DateTime.TryParse(dataHoraStr, out DateTime dataHora))
            {
                Console.WriteLine("Data/hora inválida. Tente novamente.");
                continue;
            }

            Console.Write("Digite o título do compromisso: ");
            string titulo = Console.ReadLine() ?? "";

            Console.WriteLine("Escolha o fuso horário de origem:");
            for (int i = 0; i < fusosDisponiveis.Count; i++)
            {
                Console.WriteLine($"{i} - {fusosDisponiveis[i]}");
            }

            Console.Write("Digite o número correspondente ao fuso horário: ");
            if (!int.TryParse(Console.ReadLine(), out int indiceFuso) || indiceFuso < 0 || indiceFuso >= fusosDisponiveis.Count)
            {
                Console.WriteLine("Escolha inválida. Tente novamente.\n");
                continue;
            }

            string fusoOrigem = fusosDisponiveis[indiceFuso];

            try
            {
                var dataUtc = TimeZoneInfo.ConvertTimeToUtc(dataHora, TimeZoneInfo.FindSystemTimeZoneById(fusoOrigem));
                agenda.Add(new AgendaEntrada(dataUtc, titulo, conversor));
                Console.WriteLine("Compromisso adicionado com sucesso!\n");
            }
            catch
            {
                Console.WriteLine("Fuso horário inválido. Tente novamente.\n");
            }

            Console.Write("Deseja adicionar outro compromisso? (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s") break;
        }

        Console.WriteLine("\n*** Visualização de Compromissos ***");

        Console.WriteLine("Escolha o fuso horário para exibir os compromissos:");
        for (int i = 0; i < fusosDisponiveis.Count; i++)
        {
            Console.WriteLine($"{i} - {fusosDisponiveis[i]}");
        }

        Console.Write("Digite o número correspondente ao fuso horário de destino: ");
        if (!int.TryParse(Console.ReadLine(), out int indiceDestino) || indiceDestino < 0 || indiceDestino >= fusosDisponiveis.Count)
        {
            Console.WriteLine("Escolha inválida. Encerrando.");
            return;
        }

        string fusoDestino = fusosDisponiveis[indiceDestino];

        Console.Write("Digite a data desejada para filtrar (ex: 2025-08-14): ");
        string dataFiltroStr = Console.ReadLine() ?? "";
        if (!DateTime.TryParse(dataFiltroStr, out DateTime dataFiltro))
        {
            Console.WriteLine("Data inválida.");
            return;
        }

        Console.WriteLine($"\nCompromissos em {fusoDestino} para o dia {dataFiltro:dd/MM/yyyy}:");
        foreach (var entrada in agenda)
        {
            var dataConvertida = conversor.ConverterParaFusoHorario(entrada.DataHora, fusoDestino);
            if (dataConvertida.Date == dataFiltro.Date)
            {
                entrada.Imprimir(fusoDestino);
            }
        }
    }
}
