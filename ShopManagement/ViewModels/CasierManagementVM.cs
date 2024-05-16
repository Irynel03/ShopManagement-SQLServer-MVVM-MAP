using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    internal class CasierManagementVM : INotifyPropertyChanged
    {
        //GeneralBLL bsLogic = new GeneralBLL();
        public CasierBL casierBL { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public CasierManagementVM(int IdCasier)
        {
            casierBL = new CasierBL(IdCasier);
            

        }
        public CasierManagementVM()
        {
            casierBL = new CasierBL(2);
            


        }

        private RelayCommand cautaSiAdaugaProdusPeBonCommand;
        public ICommand CautaSiAdaugaProdusPeBonCommand
        {
            get
            {
                if (cautaSiAdaugaProdusPeBonCommand == null)
                {
                    cautaSiAdaugaProdusPeBonCommand = new RelayCommand(CautaSiAdaugaProdusPeBon);
                }

                return cautaSiAdaugaProdusPeBonCommand;
            }
        }
        
        private void CautaSiAdaugaProdusPeBon(object commandParameter)
        {
            casierBL.CautaSiAdaugaProdusPeBon(produsSearchText);
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private string produsSearchText;

        public string ProdusSearchText 
        { get => produsSearchText; set => SetProperty(ref produsSearchText, value); }

        private string dataExpirareFiltrareText;

        public string DataExpirareFiltrareText { get => dataExpirareFiltrareText; set => SetProperty(ref dataExpirareFiltrareText, value); }

        private RelayCommand filtreazaDupaDataExpirareCommand;

        public ICommand FiltreazaDupaDataExpirareCommand
        {
            get
            {
                if (filtreazaDupaDataExpirareCommand == null)
                {
                    filtreazaDupaDataExpirareCommand = new RelayCommand(FiltreazaDupaDataExpirare);
                }

                return filtreazaDupaDataExpirareCommand;
            }
        }

        private void FiltreazaDupaDataExpirare(object commandParameter)
        {
            DateTime dataExpirare;
            if (DateTime.TryParseExact(dataExpirareFiltrareText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataExpirare))
            {
                if(dataExpirare > DateTime.Now)
                {
                    casierBL.FiltreazaDupaDataExpirare(dataExpirare);
                }
                else
                    MessageBox.Show("Data nu este una valida");
            }
            else
            {
                MessageBox.Show("Data nu este introdusa corect");
            }
        }
        private string producatorFiltrareText;

        public string ProducatorFiltrareText { get => producatorFiltrareText; set => SetProperty(ref producatorFiltrareText, value); }

        private RelayCommand filtreazaDupaProducatorCommand;

        public ICommand FiltreazaDupaProducatorCommand
        {
            get
            {
                if (filtreazaDupaProducatorCommand == null)
                {
                    filtreazaDupaProducatorCommand = new RelayCommand(FiltreazaDupaProducator);
                }

                return filtreazaDupaProducatorCommand;
            }
        }

        private void FiltreazaDupaProducator(object commandParameter)
        {
        }

        private string categorieFiltrareText;

        public string CategorieFiltrareText { get => categorieFiltrareText; set => SetProperty(ref categorieFiltrareText, value); }

        private RelayCommand filtreazaDupaCategorieCommand;

        public ICommand FiltreazaDupaCategorieCommand
        {
            get
            {
                if (filtreazaDupaCategorieCommand == null)
                {
                    filtreazaDupaCategorieCommand = new RelayCommand(FiltreazaDupaCategorie);
                }

                return filtreazaDupaCategorieCommand;
            }
        }

        private void FiltreazaDupaCategorie(object commandParameter)
        {




        }
    }
}
