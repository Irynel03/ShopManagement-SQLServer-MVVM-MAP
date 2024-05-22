using ShopManagement.Models.BusinessLogic;
using ShopManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ShopManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string numeUtilizator = NumeLoginTB.Text;
            string parolaUtilizator = ParolaLoginPB.Password;

            GeneralBLL gbl = new GeneralBLL();
            List<Tuple<string, string, string, int>> utilizatori = gbl.GetUtilizatoriData();

            bool utilizatorGasit = false;
            foreach (var utilizator in utilizatori)
            {
                if (utilizator.Item1.ToString() == numeUtilizator && utilizator.Item2.ToString() == parolaUtilizator)
                {
                    utilizatorGasit = true;
                    if (utilizator.Item3 == "Casier")
                    {
                        MessageBox.Show("de implementat");



                        var casierManagementVM = new CasierManagementVM(Convert.ToInt32(utilizator.Item4));

                        CasierMenuGrid.DataContext = casierManagementVM;
                        CasierMenuGrid.Visibility = Visibility.Visible;
                        MainMenuGrid.Visibility = Visibility.Collapsed;

                    }
                    else
                    {
                        var adminManagementVM = new AdminManagementVM();

                        this.DataContext = adminManagementVM;
                        AdminMenuGrid.Visibility = Visibility.Visible;
                        MainMenuGrid.Visibility = Visibility.Collapsed;
                    }
                    break;
                }
            }
            if (!utilizatorGasit)
            {
                ParolaLoginPB.Password = "";
                MessageBox.Show("Datele introduse nu corespund, mai incearca!");
            }
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

        private void listBoxProduseScanate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdaugaProdusBon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVisualizeData_Click(object sender, RoutedEventArgs e)
        {
            VisualizeDataGrid.Visibility = Visibility.Visible;
            AdminMenuGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModificaData_Click(object sender, RoutedEventArgs e)
        {
            // ---------------------- aci de modificas

            SetActivityGrid.Visibility = Visibility.Visible;
            //ModifyGrid.Visibility = Visibility.Visible;
            AdminMenuGrid.Visibility = Visibility.Collapsed;
        }





        private void btnModificaDateProdus_Click(object sender, RoutedEventArgs e)
        {
            ModifyProdusGrid.Visibility = Visibility.Visible;
            ModifyGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModificaDataUtilizator_Click(object sender, RoutedEventArgs e)
        {
            ModifyUtilizatorGrid.Visibility = Visibility.Visible;
            ModifyGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModificaDateProducator_Click(object sender, RoutedEventArgs e)
        {
            ModifyProducatorGrid.Visibility = Visibility.Visible;
            ModifyGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModificaDateStocProdus_Click(object sender, RoutedEventArgs e)
        {
            ModifyStocProdusGrid.Visibility = Visibility.Visible;
            ModifyGrid.Visibility = Visibility.Collapsed;
        }
    }
}
