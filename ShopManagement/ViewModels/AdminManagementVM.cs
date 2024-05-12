using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    internal class AdminManagementVM
    {
        GeneralBLL bsLogic = new GeneralBLL();







        public void AfisareSumeIncasatePeZileTimpDeOLuna(object obj)
        {
            // Verifică dacă obiectul primit ca parametru este un tuple cu doi parametri de tip int (idCasier și luna)
            if (obj is Tuple<string, string> tupleParams)
            {
                // Extrage parametrii din tuple
                int idCasier = System.Convert.ToInt32(tupleParams.Item1);
                int luna = System.Convert.ToInt32(tupleParams.Item2);

                // Obține lista de sume încasate pe zile pentru casierul și luna specificate
                List<string> sumePeZile = bsLogic.GetSumePeZileTimpDeOLuna(idCasier, luna);

                // Verifică dacă lista de sume este goală
                if (sumePeZile.Count > 0)
                {
                    // Construiește un șir de caractere care conține toate sumele încasate pe zile, separate prin linii noi
                    string sumeText = string.Join(Environment.NewLine, sumePeZile);

                    // Afisează sumele încasate pe zile într-un MessageBox
                    MessageBox.Show(sumeText, "Sume încasate pe zile timp de o lună", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Afisează un mesaj de informare dacă nu există sume încasate pentru luna specificată
                    MessageBox.Show("Nu există sume încasate pentru luna specificată.", "Informare", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // Afisează un mesaj de eroare dacă obiectul primit nu este un tuple cu doi parametri de tip int
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
            int producatorId = System.Convert.ToInt32(obj.ToString());
            List<string> listaProduse = bsLogic.GetProduseDeLaProducatorul(producatorId);
            string listaProduseString = string.Join(Environment.NewLine, listaProduse);

            MessageBox.Show(listaProduseString);
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

        #endregion
    }
}
