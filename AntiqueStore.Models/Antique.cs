namespace AntiqueStore.Models
{
    public abstract class Antique
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public abstract string GetFullInfo();

        public string FullInfo => GetFullInfo();
    }
}