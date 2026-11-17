using System;
using System.Windows;
using AntiqueStore.Models;

namespace AntiqueStore.PL
{
    public partial class AddItemWindow : Window
    {
        // Это свойство мы передадим в главное окно! (Передача данных между формами)
        public Antique CreatedItem { get; private set; }

        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int year = int.Parse(YearBox.Text);
                decimal price = decimal.Parse(PriceBox.Text);

                if (TypeComboBox.SelectedIndex == 0) // Картина
                {
                    CreatedItem = new Painting
                    {
                        Name = NameBox.Text,
                        Year = year,
                        Price = price,
                        Artist = DetailsBox.Text,
                        Technique = "Масло",
                        Description = "Картина"
                    };
                }
                else // Монета
                {
                    CreatedItem = new Coin
                    {
                        Name = NameBox.Text,
                        Year = year,
                        Price = price,
                        Country = DetailsBox.Text,
                        Metal = "Серебро",
                        Description = "Монета"
                    };
                }

                this.DialogResult = true; // Сообщаем главному окну, что сохранение успешно
                this.Close();
            }
            catch
            {
                MessageBox.Show("Пожалуйста, проверьте правильность ввода года и цены (только числа).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}