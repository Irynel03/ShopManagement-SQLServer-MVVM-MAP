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

                        NumeLoginTB.Text = "";
                        ParolaLoginPB.Password = "";
                    }
                    else
                    {
                        var adminManagementVM = new AdminManagementVM();

                        this.DataContext = adminManagementVM;
                        AdminMenuGrid.Visibility = Visibility.Visible;
                        MainMenuGrid.Visibility = Visibility.Collapsed;

                        NumeLoginTB.Text = "";
                        ParolaLoginPB.Password = "";
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
            ModifyGrid.Visibility = Visibility.Visible;
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

        private void btnModificaActivity_Click(object sender, RoutedEventArgs e)
        {
            SetActivityGrid.Visibility = Visibility.Visible;
            AdminMenuGrid.Visibility = Visibility.Collapsed;
        }

        private void btnBackToAdminMenuFromAddProdus_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Visible;
            AddProdusGrid.Visibility = Visibility.Collapsed;
        }

        private void btnGoToLoginFromAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Collapsed;
            MainMenuGrid.Visibility = Visibility.Visible;
        }

        private void btnBackToLoginCasier_Click(object sender, RoutedEventArgs e)
        {
            CasierMenuGrid.Visibility = Visibility.Collapsed;
            MainMenuGrid.Visibility = Visibility.Visible;
        }

        private void btnAddStocProdusBackToAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Visible;
            AddStocProdusGrid.Visibility = Visibility.Collapsed;
        }

        private void btnAddProducatorBackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Visible;
            AddProducatorGrid.Visibility = Visibility.Collapsed;
        }

        private void btnSetActivityBackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Visible;
            SetActivityGrid.Visibility = Visibility.Collapsed;
        }

        private void btnVisualizeDataBackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            VisualizeDataGrid.Visibility = Visibility.Collapsed;
            AdminMenuGrid.Visibility = Visibility.Visible;
        }

        private void btnModifyBackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Visible;
            ModifyGrid.Visibility = Visibility.Collapsed;
        }

        private void btnAddUtilizatorBackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuGrid.Visibility = Visibility.Visible;
            AddUtilizatorGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModifyProdusBackToModidy_Click(object sender, RoutedEventArgs e)
        {
            ModifyGrid.Visibility = Visibility.Visible;
            ModifyProdusGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModifyProducatorBackToModidy_Click(object sender, RoutedEventArgs e)
        {
            ModifyGrid.Visibility = Visibility.Visible;
            ModifyProducatorGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModifyStocProdusBackToModidy_Click(object sender, RoutedEventArgs e)
        {
            ModifyGrid.Visibility = Visibility.Visible;
            ModifyStocProdusGrid.Visibility = Visibility.Collapsed;
        }

        private void btnModifyUtilizatorBackToModidy_Click(object sender, RoutedEventArgs e)
        {
            ModifyGrid.Visibility = Visibility.Visible;
            ModifyUtilizatorGrid.Visibility = Visibility.Collapsed;
        }
    }
}
