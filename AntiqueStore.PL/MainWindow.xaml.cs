using System.Windows;
using AntiqueStore.BLL;
using AntiqueStore.DAL;
using AntiqueStore.PL.ViewModels;

namespace AntiqueStore.PL
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IDataRepository repository = new JsonDataRepository();
            IInventoryService service = new InventoryService(repository);

            DataContext = new MainViewModel(service);
        }
    }
}