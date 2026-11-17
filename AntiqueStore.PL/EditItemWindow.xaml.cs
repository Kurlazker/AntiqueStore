using System;
using System.Windows;
using AntiqueStore.Models;

namespace AntiqueStore.PL
{
    public partial class EditItemWindow : Window
    {
        // Принятый на редактирование объект
        public Antique OriginalItem { get; }
        // Изменённый объект для передачи обратно
        public Antique EditedItem { get; private set; }

        public EditItemWindow(Antique item)
        {
            InitializeComponent();
            OriginalItem = item;
            FillFields();
        }

        private void FillFields()
        {
            IdText.Text = OriginalItem.Id.ToString();
            NameBox.Text = OriginalItem.Name;
            YearBox.Text = OriginalItem.Year.ToString();
            PriceBox.Text = OriginalItem.Price.ToString();

            if (OriginalItem is Painting painting)
            {
                TypeComboBox.SelectedIndex = 0;
                DetailsBox.Text = painting.Artist;
            }
            else if (OriginalItem is Coin coin)
            {
                TypeComboBox.SelectedIndex = 1;
                DetailsBox.Text = coin.Country;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int year = int.Parse(YearBox.Text);
                decimal price = decimal.Parse(PriceBox.Text);

                // Сохраняем ID оригинала!
                if (TypeComboBox.SelectedIndex == 0)
                {
                    EditedItem = new Painting
                    {
                        Id = OriginalItem.Id, // ВАЖНО: сохраняем тот же ID
                        Name = NameBox.Text,
                        Year = year,
                        Price = price,
                        Artist = DetailsBox.Text,
                        Technique = "Масло",
                        Description = "Картина"
                    };
                }
                else
                {
                    EditedItem = new Coin
                    {
                        Id = OriginalItem.Id, // ВАЖНО: сохраняем тот же ID
                        Name = NameBox.Text,
                        Year = year,
                        Price = price,
                        Country = DetailsBox.Text,
                        Metal = "Серебро",
                        Description = "Монета"
                    };
                }

                this.DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Проверьте правильность ввода года и цены.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}