namespace MovieManagement.Domain
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Lingua { get; set; } = string.Empty;
        public double Classificacao { get; set; }
    }
}
