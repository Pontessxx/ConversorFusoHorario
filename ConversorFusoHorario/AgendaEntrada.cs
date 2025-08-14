using System;

namespace ConversorFusoHorario
{
    public class AgendaEntrada : IAgendaEntrada
    {
        public DateTime DataHora { get; set; }
        public string Titulo { get; set; }

        private readonly IConversorHora _conversor;

        public AgendaEntrada(DateTime dataHora, string titulo, IConversorHora conversor)
        {
            DataHora = dataHora;
            Titulo = titulo;
            _conversor = conversor;
        }

        public void Imprimir(string? idFusoDestino = null)
        {
            var dataConvertida = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;

            Console.WriteLine($"{dataConvertida:G} - {Titulo}");
        }

        public void ImprimirHora(string? idFusoDestino = null)
        {
            var dataConvertida = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;

            Console.WriteLine(dataConvertida.ToString("HH:mm"));
        }

        public void ImprimirDia(string? idFusoDestino = null)
        {
            var dataConvertida = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;

            Console.WriteLine(dataConvertida.ToString("dd/MM/yyyy"));
        }

        public void ImprimirDiaSemana(string? idFusoDestino = null)
        {
            var dataConvertida = idFusoDestino != null
                ? _conversor.ConverterParaFusoHorario(DataHora, idFusoDestino)
                : DataHora;

            Console.WriteLine(dataConvertida.DayOfWeek);
        }
    }
}
