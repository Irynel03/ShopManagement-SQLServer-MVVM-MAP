using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.Models.BusinessLogic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    internal class CasierManagementVM : INotifyPropertyChanged
    {
        public CasierBL casierBL { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public CasierManagementVM(int IdCasier)
        {
            casierBL = new CasierBL(IdCasier);

            dataExpirareFiltrare = DateTime.Now;
        }
        public CasierManagementVM()
        {
            casierBL = new CasierBL(1);
            dataExpirareFiltrare = DateTime.Now;
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
        private ICommand _stergeProdusCommand;
        public ICommand StergeProdusCommand
        {
            get
            {
                if (_stergeProdusCommand == null)
                {
                    _stergeProdusCommand = new RelayCommand(StergeProdus);
                }
                return _stergeProdusCommand;
            }
        }

        private void StergeProdus(object parameter)
        {
            if (parameter is Tuple<string, BonProdus> produs)
            {
                casierBL.StergeProdusDePeBon(produs);
            }
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

        private DateTime dataExpirareFiltrare;

        public DateTime DataExpirareFiltrare { get => dataExpirareFiltrare; set => SetProperty(ref dataExpirareFiltrare, value); }

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
            
                if(dataExpirareFiltrare < DateTime.Now)
                {
                    casierBL.FiltreazaDupaDataExpirare(dataExpirareFiltrare);
                }
                else
                    MessageBox.Show("Data nu este una valida");
            
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
            casierBL.FiltreazaDupaProducator(producatorFiltrareText);
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
            casierBL.FiltreazaDupaCategorie(categorieFiltrareText);
        }

        private RelayCommand resetareFiltrareProduseCommand;

        public ICommand ResetareFiltrareProduseCommand
        {
            get
            {
                if (resetareFiltrareProduseCommand == null)
                {
                    resetareFiltrareProduseCommand = new RelayCommand(ResetareFiltrareProduse);
                }

                return resetareFiltrareProduseCommand;
            }
        }

        private void ResetareFiltrareProduse(object commandParameter)
        {
            casierBL.ResetareFiltrareProduse();
        }

        private RelayCommand finalizeazaBonFiscalCommand;

        public ICommand FinalizeazaBonFiscalCommand
        {
            get
            {
                if (finalizeazaBonFiscalCommand == null)
                {
                    finalizeazaBonFiscalCommand = new RelayCommand(FinalizeazaBonFiscal);
                }

                return finalizeazaBonFiscalCommand;
            }
        }

        private void FinalizeazaBonFiscal(object commandParameter)
        {
            casierBL.FinalizeazaBonFiscal();
        }

    }
}
