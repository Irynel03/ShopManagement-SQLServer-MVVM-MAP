using ShopManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ShopManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoginAsAdmin_Click(object sender, RoutedEventArgs e)
        {
            var adminManagementVM = new AdminManagementVM();

            AdminMenuGrid.DataContext = adminManagementVM;
            AdminMenuGrid.Visibility = Visibility.Visible;
            MainMenuGrid.Visibility = Visibility.Collapsed;
        }

        private void btnLoginAsCasier_Click(object sender, RoutedEventArgs e)
        {



        }

        private void btnAdaugaProdus_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Collapsed;
            AddProdusGrid.Visibility = Visibility.Visible;
        }

        private void btnAfisarePreturiProduseDupaCategoriaSpecificata_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAfisareSumeIncasateDeCasier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdaugareUtilizator_Click(object sender, RoutedEventArgs e)
        {
            AddUtilizatorGrid.Visibility = Visibility.Visible;
            AdminMenuGrid.Visibility = Visibility.Collapsed;
        }

        private void btnAdaugaStocProdus_Click(object sender, RoutedEventArgs e)
        {

            AddStocProdusGrid.Visibility = Visibility.Visible;
            AdminMenuGrid.Visibility = Visibility.Collapsed;
        }

        private void btnSelectareProdListareProduse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAfisareCelMaiMareBonZiSelectata_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdaugaProducator_Click(object sender, RoutedEventArgs e)
        {

            AddProducatorGrid.Visibility = Visibility.Visible;
            AdminMenuGrid.Visibility = Visibility.Collapsed;
        }
    }
}
