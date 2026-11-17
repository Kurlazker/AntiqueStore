using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AntiqueStore.BLL;
using AntiqueStore.Models;

namespace AntiqueStore.PL.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IInventoryService _service;
        private ObservableCollection<Antique> _items;
        private Antique _selectedItem;
        private string _searchText;

        public ObservableCollection<Antique> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public Antique SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public decimal TotalValue { get; private set; }
        public int TotalCount { get; private set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel(IInventoryService service)
        {
            _service = service;
            _items = new ObservableCollection<Antique>();
            _searchText = string.Empty;

            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(SearchItems);
            RefreshCommand = new RelayCommand((obj) => LoadItems());

            LoadItems();
        }

        private void LoadItems()
        {
            Items.Clear();
            foreach (var item in _service.GetAll())
                Items.Add(item);

            TotalValue = _service.CalculateTotalValue();
            TotalCount = _service.GetCount();
            OnPropertyChanged(nameof(TotalValue));
            OnPropertyChanged(nameof(TotalCount));
        }

        // Открытие второго окна + ПЕРЕДАЧА ДАННЫХ между формами
        private void AddItem(object parameter)
        {
            AddItemWindow addWindow = new AddItemWindow();
            addWindow.Owner = Application.Current.MainWindow;

            bool? result = addWindow.ShowDialog();

            if (result == true && addWindow.CreatedItem != null)
            {
                _service.Add(addWindow.CreatedItem);
                LoadItems();
            }
        }

        private void DeleteItem(object parameter)
        {
            if (SelectedItem != null)
            {
                _service.Remove(SelectedItem.Id);
                LoadItems();
            }
        }

        private void SearchItems(object parameter)
        {
            Items.Clear();
            foreach (var item in _service.SearchByName(SearchText))
                Items.Add(item);
        }
    }
}