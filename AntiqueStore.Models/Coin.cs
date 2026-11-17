namespace AntiqueStore.Models
{
    public class Coin : Antique
    {
        public string Metal { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public override string GetFullInfo()
        {
            return $"Монета: {Name} (Страна: {Country}, Металл: {Metal}, {Year} г.)";
        }
    }
}