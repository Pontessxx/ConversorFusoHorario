using System;

namespace ConversorFusoHorario
{
    public class ConversorHora : IConversorHora
    {
        public DateTime ConverterParaFusoHorario(DateTime dataHora, string idFusoDestino)
        {
            var destino = TimeZoneInfo.FindSystemTimeZoneById(idFusoDestino);
            return TimeZoneInfo.ConvertTime(dataHora, destino);
        }

        public string ObterFusoHorarioDaData(string dataHoraStr)
        {
            if (DateTime.TryParse(dataHoraStr, out var dataHora))
            {
                var localTimeZone = TimeZoneInfo.Local;
                return localTimeZone.Id;
            }

            throw new FormatException("Data/hora inválida.");
        }
    }
}
