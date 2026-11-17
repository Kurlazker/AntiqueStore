using System.Collections.ObjectModel;
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
        private int _selectedTypeIndex;

        public string InputName { get; set; } = string.Empty;
        public int InputYear { get; set; } = 2000;
        public decimal InputPrice { get; set; }
        public string InputArtist { get; set; } = string.Empty;
        public string InputCountry { get; set; } = string.Empty;

        public int SelectedTypeIndex
        {
            get => _selectedTypeIndex;
            set => SetProperty(ref _selectedTypeIndex, value);
        }

        public ObservableCollection<Antique> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public Antique SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
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
            _selectedTypeIndex = 0;

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

        private void AddItem(object parameter)
        {
            if (SelectedTypeIndex == 0)
            {
                var item = new Painting
                {
                    Name = InputName,
                    Year = InputYear,
                    Price = InputPrice,
                    Artist = InputArtist,
                    Technique = "Масло",
                    Description = "Картина: " + InputName
                };
                _service.Add(item);
            }
            else
            {
                var item = new Coin
                {
                    Name = InputName,
                    Year = InputYear,
                    Price = InputPrice,
                    Country = InputCountry,
                    Metal = "Серебро",
                    Description = "Монета: " + InputName
                };
                _service.Add(item);
            }

            LoadItems();
            ClearFields();
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

        private void ClearFields()
        {
            InputName = string.Empty;
            InputYear = 2000;
            InputPrice = 0;
            InputArtist = string.Empty;
            InputCountry = string.Empty;
            OnPropertyChanged(nameof(InputName));
            OnPropertyChanged(nameof(InputYear));
            OnPropertyChanged(nameof(InputPrice));
            OnPropertyChanged(nameof(InputArtist));
            OnPropertyChanged(nameof(InputCountry));
        }
    }
}