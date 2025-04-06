namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        public double? lon { get; set; }
        public double? lat { get; set; }

        // temperatura mínima
        public double? temp_min { get; set; }

        // temperatura máxima
        public double? temp_max { get; set; }

        // visibilidade
        public int? visibility { get; set; }

        // velocidade do vento
        public double? speed { get; set; }

        public string? main {  get; set; }
        // descrição do clima
        public string? description { get; set; }

        public string? sunrise { get; set; }
        public string? sunset { get; set;}

    }
}
