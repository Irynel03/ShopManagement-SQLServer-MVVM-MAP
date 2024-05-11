﻿using ShopManagement.Helpers;
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
