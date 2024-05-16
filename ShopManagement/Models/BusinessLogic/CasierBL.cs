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

        public double SumaTotalaProduseScanate {  get; set; }

        private List<Produs> produse = new List<Produs>();
        private List<StocProdus> stocProduse = new List<StocProdus>();
        public ObservableCollection<Tuple<string, BonProdus>> ProduseBon { get; set; } = new ObservableCollection<Tuple<string, BonProdus>>();
        public BonFiscal bonFiscal { get; set; }

        


        public CasierBL(int idCasier)
        {
            bonFiscal = new BonFiscal(idCasier);

            produse = GetProduse();

            stocProduse = GetStocProduse();

            SumaTotalaProduseScanate = 0;

            //produse = produse.Where(x => x.IsActive).ToList();

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

                var produs = stocProduse.FirstOrDefault(p => p.IdProdus == GetProdusId(produsSearchText));
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
            foreach (var produs in ProduseBon) {
                SumaTotalaProduseScanate += produs.Item2.Subtotal;
            }
            OnPropertyChanged(nameof(SumaTotalaProduseScanate));
        }

        internal void FiltreazaDupaDataExpirare(DateTime dataExpirareParam)
        {
            foreach(var stocProdus in stocProduse) {
                if(stocProdus.DataExpirare < dataExpirareParam)
                {
                    foreach(var produs in produse)
                    {
                        if(produs.Id == stocProdus.IdProdus)
                        {
                            produse.Remove(produs);
                        }
                    }
                }
            }
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


        public List<Produs> GetProduse()
        {
            var result = context.GetProduse();
            List<Produs> produse = new List<Produs>();

            foreach (var item in result)
            {
                produse.Add(new Produs(item.Id, item.Nume.Trim(), item.Categorie, item.Producator_Id, item.IsActive));
            }
            produse = produse.Where(x => x.IsActive).ToList();
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

            // Parsarea rezultatului și obținerea numelor produselor
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
