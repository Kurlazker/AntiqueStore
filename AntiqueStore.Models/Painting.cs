namespace AntiqueStore.Models
{
    public class Painting : Antique
    {
        public string Artist { get; set; } = string.Empty;
        public string Technique { get; set; } = string.Empty;

        public override string GetFullInfo()
        {
            return $"Картина: {Name} (Художник: {Artist}, Техника: {Technique}, {Year} г.)";
        }
    }
}