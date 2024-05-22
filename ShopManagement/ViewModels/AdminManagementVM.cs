using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    internal class AdminManagementVM: INotifyPropertyChanged
    {
        public GeneralBLL bsLogic { get; set; } = new GeneralBLL();




        public AdminManagementVM()
        {
            ziBonFiltrare = DateTime.Now;
        }







        public void AfisareSumeIncasatePeZileTimpDeOLuna(object obj)
        {
            if (obj is Tuple<string, string> tupleParams)
            {
                int idCasier = System.Convert.ToInt32(tupleParams.Item1);
                int luna = System.Convert.ToInt32(tupleParams.Item2);

                List<string> sumePeZile = bsLogic.GetSumePeZileTimpDeOLuna(idCasier, luna);

                if (sumePeZile.Count > 0)
                {
                    string sumeText = string.Join(Environment.NewLine, sumePeZile);
                    MessageBox.Show(sumeText, "Sume încasate pe zile timp de o lună", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nu există sume încasate pentru luna specificată.", "Informare", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Parametrii invalizi pentru afișarea sumelor încasate pe zile.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand afisareSumeIncasatePeZileTimpDeOLunaCommand;
        public ICommand AfisareSumeIncasatePeZileTimpDeOLunaCommand
        {
            get
            {
                if (afisareSumeIncasatePeZileTimpDeOLunaCommand == null)
                {
                    afisareSumeIncasatePeZileTimpDeOLunaCommand = new RelayCommand(AfisareSumeIncasatePeZileTimpDeOLuna);
                }
                return afisareSumeIncasatePeZileTimpDeOLunaCommand;
            }
        }

        public void AfisareProduseProducator(object obj)
        {
            bsLogic.AfisareProduseProducator(obj.ToString());
        }

        private ICommand afisareProduseProducatorCommand;
        public ICommand AfisareProduseProducatorCommand
        {
            get
            {
                if (afisareProduseProducatorCommand == null)
                {
                    afisareProduseProducatorCommand = new RelayCommand(AfisareProduseProducator);
                }
                return afisareProduseProducatorCommand;
            }
        }

        public void AfisareSumaProduseDupaCategorie(object obj)
        {
            string numeCategorie = obj.ToString();

            double sumaTotala = bsLogic.GetSumaProduseStocCurentCategoria(numeCategorie);

            MessageBox.Show(sumaTotala.ToString());
        }
        private ICommand afisareSumaProduseDupaCategorieCommand;
        public ICommand AfisareSumaProduseDupaCategorieCommand
        {
            get
            {
                if(afisareSumaProduseDupaCategorieCommand == null)
                {
                    afisareSumaProduseDupaCategorieCommand = new RelayCommand(AfisareSumaProduseDupaCategorie);
                }
                return afisareSumaProduseDupaCategorieCommand;
            }
        }


        private DateTime ziBonFiltrare;
        public DateTime ZiBonFiltrare { get => ziBonFiltrare; set => SetProperty(ref ziBonFiltrare, value); }
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


        #region AddToDB ICommands

        public void AddStocProdus(Object obj)
        {
            if (obj != null)
            {
                bsLogic.AddStocProdus(obj);
            }
        }

        private ICommand addStocProdusCommand;
        public ICommand AddStocProdusCommand
        {
            get
            {
                if (addStocProdusCommand == null)
                {
                    addStocProdusCommand = new RelayCommand(AddStocProdus);
                }
                return addStocProdusCommand;
            }
        }
        public void AddOferta(Object obj)
        {
            if (obj != null)
            {
                bsLogic.AddOferta(obj);
            }
        }

        private ICommand addOfertaCommand;
        public ICommand AddOfertaCommand
        {
            get
            {
                if (addOfertaCommand == null)
                {
                    addOfertaCommand = new RelayCommand(AddOferta);
                }
                return addOfertaCommand;
            }
        }
        public void AddProducator(Object obj)
        {
            if (obj != null)
            {
                bsLogic.AddProducator(obj);
            }
        }

        private ICommand addProducatorCommand;
        public ICommand AddProducatorCommand
        {
            get
            {
                if (addProducatorCommand == null)
                {
                    addProducatorCommand = new RelayCommand(AddProducator);
                }
                return addProducatorCommand;
            }
        }

        public void AddBonProdus(Object obj)
        {
            if (obj != null)
            {
                bsLogic.AddBonProdus(obj);
            }
        }

        private ICommand addBonProdusCommand;
        public ICommand AddBonProdusCommand
        {
            get
            {
                if (addBonProdusCommand == null)
                {
                    addBonProdusCommand = new RelayCommand(AddBonProdus);
                }
                return addBonProdusCommand;
            }
        }

        public void AddBonFiscal(Object obj)
        {
            if (obj != null)
            {
                bsLogic.AddBonFiscal(obj);
            }
        }

        private ICommand addBonFiscalCommand;
        public ICommand AddBonFiscalCommand
        {
            get
            {
                if (addBonFiscalCommand == null)
                {
                    addBonFiscalCommand = new RelayCommand(AddBonFiscal);
                }
                return addBonFiscalCommand;
            }
        }

        public void AddProdus(Object obj)
        {
            if (obj != null)
            {
                bsLogic.AddProdus(obj);
            }
        }

        private ICommand addProdusCommand;
        public ICommand AddProdusCommand
        {
            get
            {
                if (addProdusCommand == null)
                {
                    addProdusCommand = new RelayCommand(AddProdus);
                }
                return addProdusCommand;
            }
        }

        public void AddUtilizator(Object obj)
        {
            if (obj != null)
            {
                bsLogic.AddUtilizator(obj);
            }
            MessageBox.Show("Adaugat");
        }

        private ICommand addUtilizatorCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddUtilizatorCommand
        {
            get
            {
                if (addUtilizatorCommand == null)
                {
                    addUtilizatorCommand = new RelayCommand(AddUtilizator);
                }
                return addUtilizatorCommand;
            }
        }

        private RelayCommand afiseazaCMMBonDinZiuaCommand;

        public ICommand AfiseazaCMMBonDinZiuaCommand
        {
            get
            {
                if (afiseazaCMMBonDinZiuaCommand == null)
                {
                    afiseazaCMMBonDinZiuaCommand = new RelayCommand(AfiseazaCMMBonDinZiua);
                }

                return afiseazaCMMBonDinZiuaCommand;
            }
        }

        private void AfiseazaCMMBonDinZiua(object commandParameter)
        {
            // nu era implementat


        }

        private RelayCommand afiseazaDateleProdusuluiCommand;

        public ICommand AfiseazaDateleProdusuluiCommand
        {
            get
            {
                if (afiseazaDateleProdusuluiCommand == null)
                {
                    afiseazaDateleProdusuluiCommand = new RelayCommand(AfiseazaDateleProdusului);
                }

                return afiseazaDateleProdusuluiCommand;
            }
        }

        private void AfiseazaDateleProdusului(object commandParameter)
        {
            bsLogic.AfiseazaDateleProdusuluiVisualizeGrid(produsNameVisualizeText);


        }

        private string produsNameVisualizeText;

        public string ProdusNameVisualizeText 
        { get => produsNameVisualizeText; 
            set => SetProperty(ref produsNameVisualizeText, value); }

        private string producatorNameVisualizeText;

        public string ProducatorNameVisualizeText { get => producatorNameVisualizeText; set => SetProperty(ref producatorNameVisualizeText, value); }

        private RelayCommand afiseazaDateleProducatoruluiCommand;

        public ICommand AfiseazaDateleProducatoruluiCommand
        {
            get
            {
                if (afiseazaDateleProducatoruluiCommand == null)
                {
                    afiseazaDateleProducatoruluiCommand = new RelayCommand(AfiseazaDateleProducatorului);
                }

                return afiseazaDateleProducatoruluiCommand;
            }
        }

        private void AfiseazaDateleProducatorului(object commandParameter)
        {
            bsLogic.AfiseazaDateleProducatoruluiVisualizeGrid(producatorNameVisualizeText);
        }

        private RelayCommand afiseazaDateleUtilizatoruluiCommand;

        public ICommand AfiseazaDateleUtilizatoruluiCommand
        {
            get
            {
                if (afiseazaDateleUtilizatoruluiCommand == null)
                {
                    afiseazaDateleUtilizatoruluiCommand = new RelayCommand(AfiseazaDateleUtilizatorului);
                }

                return afiseazaDateleUtilizatoruluiCommand;
            }
        }

        private void AfiseazaDateleUtilizatorului(object commandParameter)
        {
            bsLogic.AfiseazaDateleUtilizatorului(utilizatorNameVisualizeText);
        }

        private string utilizatorNameVisualizeText;

        public string UtilizatorNameVisualizeText { get => utilizatorNameVisualizeText; set => SetProperty(ref utilizatorNameVisualizeText, value); }

        private RelayCommand afiseazaDateleBonuluiFiscalCommand;

        public ICommand AfiseazaDateleBonuluiFiscalCommand
        {
            get
            {
                if (afiseazaDateleBonuluiFiscalCommand == null)
                {
                    afiseazaDateleBonuluiFiscalCommand = new RelayCommand(AfiseazaDateleBonuluiFiscal);
                }

                return afiseazaDateleBonuluiFiscalCommand;
            }
        }

        private void AfiseazaDateleBonuluiFiscal(object commandParameter)
        {
            int idBonFiscal;

            // Încearcă să convertești bonFiscalIdVisualizeText într-un număr întreg
            bool conversieReusita = int.TryParse(bonFiscalIdVisualizeText, out idBonFiscal);

            if (conversieReusita)
            {
                bsLogic.AfisareDateBonFiscal(idBonFiscal);
            }
            else
            {
                MessageBox.Show("Nu ai introdus corect un Id");
            }

        }

        private string bonFiscalIdVisualizeText;

        public string BonFiscalIdVisualizeText { get => bonFiscalIdVisualizeText; set => SetProperty(ref bonFiscalIdVisualizeText, value); }

        private string stocProdusIdVisualizeText;

        public string StocProdusIdVisualizeText { get => stocProdusIdVisualizeText; set => SetProperty(ref stocProdusIdVisualizeText, value); }

        private RelayCommand afiseazaDateStocProdusCommand;

        public ICommand AfiseazaDateStocProdusCommand
        {
            get
            {
                if (afiseazaDateStocProdusCommand == null)
                {
                    afiseazaDateStocProdusCommand = new RelayCommand(AfiseazaDateStocProdus);
                }

                return afiseazaDateStocProdusCommand;
            }
        }

        private void AfiseazaDateStocProdus(object commandParameter)
        {
            int idStocProdus;

            // Încearcă să convertești bonFiscalIdVisualizeText într-un număr întreg
            bool conversieReusita = int.TryParse(stocProdusIdVisualizeText, out idStocProdus);

            if (conversieReusita)
            {
                bsLogic.AfisareDateStocProdus(idStocProdus);
            }
            else
            {
                MessageBox.Show("Nu ai introdus corect un Id");
            }
        }

        private string numeUtilizatorModifyText;

        public string NumeUtilizatorModifyText { get => numeUtilizatorModifyText; set => SetProperty(ref numeUtilizatorModifyText, value); }

        private RelayCommand modificaActivitateaProdusuluiCommand;

        public ICommand ModificaActivitateaProdusuluiCommand
        {
            get
            {
                if (modificaActivitateaProdusuluiCommand == null)
                {
                    modificaActivitateaProdusuluiCommand = new RelayCommand(ModificaActivitateaProdusului);
                }

                return modificaActivitateaProdusuluiCommand;
            }
        }

        private void ModificaActivitateaProdusului(object commandParameter)
        {
            bsLogic.ModificaActivitateaProdusului(produsNameVisualizeText);
        }

        private RelayCommand modificaActivitateaProducatoruluiCommand;

        public ICommand ModificaActivitateaProducatoruluiCommand
        {
            get
            {
                if (modificaActivitateaProducatoruluiCommand == null)
                {
                    modificaActivitateaProducatoruluiCommand = new RelayCommand(ModificaActivitateaProducatorului);
                }

                return modificaActivitateaProducatoruluiCommand;
            }
        }

        private void ModificaActivitateaProducatorului(object commandParameter)
        {
            bsLogic.ModificaActivitateaProducatorului(producatorNameVisualizeText);
        }

        private RelayCommand modificaActivitateaUtilizatoruluiCommand;

        public ICommand ModificaActivitateaUtilizatoruluiCommand
        {
            get
            {
                if (modificaActivitateaUtilizatoruluiCommand == null)
                {
                    modificaActivitateaUtilizatoruluiCommand = new RelayCommand(ModificaActivitateaUtilizatorului);
                }

                return modificaActivitateaUtilizatoruluiCommand;
            }
        }

        private void ModificaActivitateaUtilizatorului(object commandParameter)
        {
        }

        private RelayCommand modificaActivitateaStocProdusCommand;

        public ICommand ModificaActivitateaStocProdusCommand
        {
            get
            {
                if (modificaActivitateaStocProdusCommand == null)
                {
                    modificaActivitateaStocProdusCommand = new RelayCommand(ModificaActivitateaStocProdus);
                }

                return modificaActivitateaStocProdusCommand;
            }
        }

        private void ModificaActivitateaStocProdus(object commandParameter)
        {
        }
        #endregion
    }
}
