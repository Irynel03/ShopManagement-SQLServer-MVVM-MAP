using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManagement.Models.BusinessLogic
{
    internal class CasierBL : INotifyPropertyChanged
    {
        private ShopMngEntities2 context = new ShopMngEntities2();

        public double SumaTotalaProduseScanate { get; set; }

        public List<Produs> produse { get; set; } = new List<Produs>();
        public List<String> numeProduse { get; set; } = new List<String>();
        public List<String> listaProducatori { get; set; } = new List<String>();
        public List<String> listaCateg { get; set; } = new List<String>();
        private List<StocProdus> stocProduse = new List<StocProdus>();
        public ObservableCollection<Tuple<string, BonProdus>> ProduseBon { get; set; } = new ObservableCollection<Tuple<string, BonProdus>>();
        public BonFiscal bonFiscal { get; set; }
        private int idCasier {  get; set; }




        public CasierBL(int idCasier)
        {
            bonFiscal = new BonFiscal(idCasier);
            this.idCasier = idCasier;

            stocProduse = GetStocProduse();

            produse = GetProduse();

            UpdateListaNumeProduse();
            foreach (var prod in produse)
            {
                if(!listaCateg.Contains(prod.Categorie))
                    listaCateg.Add(prod.Categorie);
            }
            List<Tuple<string, int>> listaProd = GetListaProducatori();
            foreach (var prod in listaProd)
                listaProducatori.Add(prod.Item1);


            context.ActualizeazaStatusProdus();
            SumaTotalaProduseScanate = 0;
        }
        private void UpdateListaNumeProduse()
        {
            numeProduse.Clear();
            foreach (var prod in produse)
                numeProduse.Add(prod.Nume);
        }
        private void UpdatareStocProduse()
        {
            throw new NotImplementedException();
        }

        private int GetProdusId(string numeProdus)
        {
            foreach (var produs in produse)
            {
                if (produs.Nume == numeProdus)
                {
                    return produs.Id;
                }
            }
            return -1;
        }

        private bool ProdusPeStoc(string numeProdus)
        {
            int idProdus = GetProdusId(numeProdus);
            return stocProduse.Any(p => p.IdProdus == idProdus && p.Cantitate >= 1);

        }

        public void CautaSiAdaugaProdusPeBon(string produsSearchText)
        {
            if (produse.Any(p => p.Nume == produsSearchText) && ProdusPeStoc(produsSearchText))
            {

                var produs = stocProduse.Where(p => p.IdProdus == GetProdusId(produsSearchText)).OrderBy(p => p.DataExpirare).FirstOrDefault();
                produs.Cantitate--;

                if (!ProduseBon.Any(pb => pb.Item2.IdProdus == produs.IdProdus))
                    ProduseBon.Add(new Tuple<string, BonProdus>(produsSearchText, new BonProdus(bonFiscal.IdBon, produs.IdProdus, produs.PretVanzare)));
                else
                {
                    var produsBon = ProduseBon.FirstOrDefault(sp => sp.Item2.IdProdus == produs.IdProdus);
                    produsBon.Item2.Cantitate++;
                    produsBon.Item2.Subtotal += produs.PretVanzare;
                }

                CalculeazaSumaTotalaDePeBon();
            }
            else
            {
                MessageBox.Show("Produsul Nu este pe stoc");
            }

        }
        private void CalculeazaSumaTotalaDePeBon()
        {
            SumaTotalaProduseScanate = 0;
            foreach (var produs in ProduseBon)
            {
                SumaTotalaProduseScanate += produs.Item2.Subtotal;
            }
            OnPropertyChanged(nameof(SumaTotalaProduseScanate));
        }

        internal void FiltreazaDupaDataExpirare(DateTime dataExpirareParam)
        {
            foreach (var stocProdus in stocProduse)
            {
                if (stocProdus.DataExpirare < dataExpirareParam)
                {
                    foreach (var produs in produse)
                    {
                        if (produs.Id == stocProdus.IdProdus)
                        {
                            produse.Remove(produs);
                        }
                    }
                }
            }
            UpdateListaNumeProduse();
        }

        private bool CategoriaExista(string categorie)
        {
            List<string> categList = GetListaCategorii();
            return categList.Contains(categorie);
        }
        internal void FiltreazaDupaCategorie(string categorieFiltrareText)
        {
            if (CategoriaExista(categorieFiltrareText))
            {
                MessageBox.Show("Exista categoria");
                produse = produse.Where(p => p.Categorie == categorieFiltrareText).ToList();
                UpdateListaNumeProduse();
            }
            else
            {
                MessageBox.Show("Nu exista categoria");
            }
        }

        internal void FiltreazaDupaProducator(string producatorFiltrareText)
        {
            List<Tuple<string, int>> listaProd = GetListaProducatori();
            
            if(listaProd.Any(prod=>prod.Item1 == producatorFiltrareText))
            {
                MessageBox.Show("Producatorul exista.");
                foreach (var producator in listaProd) 
                {
                    if(producator.Item1 == producatorFiltrareText)
                    {
                        produse = produse.Where(p => p.Producator_Id == producator.Item2).ToList();
                        break;
                    }
                }
                UpdateListaNumeProduse();
            }
            else
            {
                MessageBox.Show("Producatorul NU exista.");
            }
        }

        internal void ResetareFiltrareProduse()
        {
            produse = GetProduse();
        }

        internal void FinalizeazaBonFiscal()
        {
            if(ProduseBon.Count == 0)
            {
                MessageBox.Show("Nu ai scanat niciun produs.");
                return;
            }

            context.AdaugareBonFiscal(idCasier, SumaTotalaProduseScanate);
            context.UpdateStocProdusIsActiveOnConditions();

            int? idBonFiscalNullable = context.GetLastBonFiscalId().FirstOrDefault();
            int idBonFiscal = idBonFiscalNullable ?? default(int);

            foreach (var produsBon in ProduseBon)
            {
                // daca adaug mai multe produse la al doilea se strica de tot pk-ul pentru ca nu isi face update dupa crearea primului bonProdus
                context.AdaugareBonProdus(idBonFiscal, produsBon.Item2.IdProdus, produsBon.Item2.Cantitate, produsBon.Item2.Subtotal);
            }
            foreach(var stocProdus in stocProduse)
            {
                context.UpdateCantitateStocProdus(stocProdus.IdStocProdus, stocProdus.Cantitate);
            }

            context.ActualizeazaStatusProdus();

            InitializareDupaFinalizareBon();

            // bonFiscal, BonProdus, UpdateCantitateStocProdus, UpdateStatusProdus
        }
        private void InitializareDupaFinalizareBon()
        {
            stocProduse = GetStocProduse();
            //UpdatareStocProduse();

            produse = GetProduse();
            UpdateListaNumeProduse();
            ProduseBon.Clear();
            bonFiscal = new BonFiscal(idCasier);
            CalculeazaSumaTotalaDePeBon();
        }















































        public List<StocProdus> GetStocProduse()
        {
            var result = context.GetStocProduse();
            List<StocProdus> stocProduse = new List<StocProdus>();

            foreach (var item in result)
            {
                stocProduse.Add(new StocProdus(item.IdStocProdus, item.IdProdus, item.Cantitate, item.DataAprovizionare,
                    item.DataExpirare, item.UnitateMasura.Trim(), item.PretAchizitie, item.PretVanzare));
            }

            return stocProduse;
        }

        public List<Tuple<string, int>> GetListaProducatori()
        {
            var result = context.GetProducatori();
            List<Tuple<string, int>> producatoriExistenti = new List<Tuple<string, int>>();
            foreach (var item in result)
            {
                producatoriExistenti.Add(new Tuple<string, int>(item.NumeProducator.Trim(), item.Id));
            }
            return producatoriExistenti;
        }

        public List<string> GetListaCategorii()
        {
            var result = context.GetProduse();      // de modificat
            List<string> categExistente = new List<string>();
            foreach (var item in result)
            {
                if (!categExistente.Contains(item.Categorie.Trim()))
                {
                    categExistente.Add(item.Categorie.Trim());
                }

            }
            return categExistente;

        }
        public List<Produs> GetProduse()
        {
            var result = context.GetProduse();
            List<Produs> produse = new List<Produs>();

            foreach (var item in result)
            {
                produse.Add(new Produs(item.Id, item.Nume.Trim(), item.Categorie, item.Producator_Id, item.IsActive));
            }
            produse = produse.Where(x => x.IsActive).ToList();
            UpdateListaNumeProduse();
            return produse;
        }

        public List<Tuple<string, string, string, int>> GetUtilizatoriData()
        {
            var result = context.SelectUtilizatori();

            List<Tuple<string, string, string, int>> dateUtilizatori = new List<Tuple<string, string, string, int>>();

            foreach (var item in result)
            {
                dateUtilizatori.Add(Tuple.Create(item.Nume.Trim(), item.Parola.Trim(), item.Tip.Trim(), item.IdUtilizator));
            }

            return dateUtilizatori;
        }


        public List<string> GetProduseDeLaProducatorul(int producatorId)
        {
            //int? producatorId = context.Producatori.FirstOrDefault(p => p.Nume == numeProducator)?.Id;

            var result = context.GetProduseDeLaProducator(producatorId);

            List<string> numeProduse = result.Select(r => r.Nume).ToList();

            return numeProduse;


        }



        public void AddBonFiscal(object obj)
        {
            Tuple<int, double> bonFiscal = obj as Tuple<int, double>;

            // nevoie de verificare daca exista IdCasier
            if (bonFiscal.Item2 >= 0)
            {
                context.AdaugareBonFiscal(bonFiscal.Item1, bonFiscal.Item2);
            }
        }
        public void AddBonProdus(object obj)
        {
            Tuple<int, int, int, double> bonFiscal = obj as Tuple<int, int, int, double>;

            //nevoie de verificare suplimentara
            if (bonFiscal.Item3 >= 1 && bonFiscal.Item4 >= 1)
            {
                context.AdaugareBonProdus(bonFiscal.Item1, bonFiscal.Item2, bonFiscal.Item3, bonFiscal.Item4);
            }
        }



















        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
