using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ShopManagement.Models.BusinessLogic
{
    public class GeneralBLL
    {
        private ShopMngEntities2 context = new ShopMngEntities2();

        public List<Produs> produse2 { get; set; } = new List<Produs>();
        public List<String> numeProduse { get; set; } = new List<String>();
        public List<String> listaCateg { get; set; } = new List<String>();
        public List<String> listaProducatori { get; set; } = new List<String>();
        public List<String> listaUtilizatori { get; set; } = new List<String>();


        public string ErrorMessage { get; set; }




        public GeneralBLL()
        {
            VerrificareStocuri();
            var produse = context.GetProduse();

            produse2 = GetProduse();
            foreach (var prod in produse)
            {
                numeProduse.Add(prod.Nume.Trim());
                if (!listaCateg.Contains(prod.Categorie))
                    listaCateg.Add(prod.Categorie);
            }

            var listaUtil = context.SelectUtilizatori();

            foreach (var utilizator in listaUtil)
            {
                listaUtilizatori.Add(utilizator.Nume.Trim());
            }

            List<Tuple<string, int>> listaProd = GetListaProducatori();
            foreach (var prod in listaProd)
                listaProducatori.Add(prod.Item1);

        }

        private void VerrificareStocuri()
        {
            var stocProduse = context.GetStocProduse();
            foreach (var stocProdus in stocProduse)
            {
                if(stocProdus.DataExpirare < DateTime.Now && stocProdus.IsActive == true)
                {
                    context.SetStocProdusActivity(stocProdus.IdStocProdus, false);
                }

            }



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

            return produse;
        }

        public List<Tuple<string, string, string, int>> GetUtilizatoriData()
        {
            var result = context.SelectUtilizatori();

            List<Tuple<string, string, string, int>> dateUtilizatori = new List<Tuple<string, string, string, int>>();

            foreach(var item in result)
            {
                dateUtilizatori.Add(Tuple.Create(item.Nume.Trim(), item.Parola.Trim(), item.Tip.Trim(), item.IdUtilizator));
            }

            return dateUtilizatori;
        }


        public List<string> GetSumePeZileTimpDeOLuna(int idCasier, int luna)
        {
            var result = context.GetIncasariCasierPeZiLuna(idCasier, luna, 2024);

            List<string> sumePeZile = new List<string>();

            foreach (var item in result)
            {
                string sumaFormatted = $"{item.SumaIncasata:N2} RON"; // Afișează suma cu două zecimale și adaugă "RON" la final
                sumePeZile.Add($"{item.Ziua}: {sumaFormatted}"); // Adaugă ziua și suma încasată formatată în listă
            }

            return sumePeZile;
        }

        public List<string> GetProduseDeLaProducatorul(int producatorId)
        {
            var result = context.GetProduseDeLaProducator(producatorId);
            List<string> numeProduse = result.Select(r => r.Nume).ToList();
            return numeProduse;
        }

        public double GetSumaProduseStocCurentCategoria(string categorie)
        {
            var result = context.CalculeazaSumaTotalaProduseCategoriaSpecifica(categorie);
            double sumaTotala = 0;

            foreach (var item in result)
            {
                sumaTotala += item.Cantitate * item.PretVanzare;
                
            }
            return sumaTotala;
        }

        internal void AfisareProduseProducator(string numeProd)
        {
            var producatori = context.GetProducatori();
            int idProducator = -1;
            foreach(var item in producatori) 
            {
                if(item.NumeProducator.Trim() == numeProd)
                {
                    idProducator = item.Id;
                    break;
                }
            }
            if(idProducator == -1) {
                MessageBox.Show("Nu exista producatorul.");
                return;
            }

            var produse = context.GetProduse();
            List<string> produseList = new List<string>();
            foreach(var item in produse)
            {
                if (item.Producator_Id == idProducator)
                    produseList.Add(item.Nume.Trim());
            }

            if (produseList.Count > 0)
                MessageBox.Show(string.Join(Environment.NewLine, produseList), "Lista de produse pentru producătorul selectat:");
            else
                MessageBox.Show("Producatorul nu are produse in istoricul magazinului");

        }


        










        public void AddUtilizator(object obj)
        {
            Tuple<string, string, string> utilizator = obj as Tuple<string, string, string>;

            if (utilizator.Item1 != "" && utilizator.Item2 != "" && (utilizator.Item3 == "Admin" || utilizator.Item3 == "Casier"))
            {
                context.AdaugareUtilizator(utilizator.Item1, utilizator.Item2, utilizator.Item3);
            }
            else
                ErrorMessage = "Date introduse gresit la utilizator";
        }

        private int GetIdProdusFromNume(string numeProd)
        {
            var produse = context.GetProduse();
            foreach (var item in produse)
            {
                if (item.Nume.Trim() == numeProd)
                    return item.Id;
            }
            return -1;
        }
        private string GetNumeProdusFromId(int id)
        {
            var produse3 = context.GetProduse();
            foreach (var item in produse3)
            {
                if (item.Id == id)
                    return item.Nume.Trim();
            }
            return "eroare";
        }

        private float CalculeazaPretFinalProdus(double pretInitial)
        {
            return ((float)(pretInitial + pretInitial * 25 / 100));
        }
        public void AddStocProdus(object obj)
        {
            Tuple<string, int, Nullable<DateTime>, Nullable<DateTime>, string, double> stocProdus = obj as Tuple<string, int, Nullable<DateTime>, Nullable<DateTime>, string, double>;

            Nullable<int> idProdus = GetIdProdusFromNume(stocProdus.Item1);
            if(idProdus == -1)
            {
                MessageBox.Show("Numele produsului nu este corect");
                return;
            }

            Nullable<float> pretFinal = CalculeazaPretFinalProdus(stocProdus.Item6);
            if (stocProdus.Item5 != "")
            {
                context.AdaugareStocProdus(idProdus, stocProdus.Item2, stocProdus.Item3, stocProdus.Item4, stocProdus.Item5, stocProdus.Item6, pretFinal, true);
            }
        }
        public void AddProdus(object obj)
        {
            Tuple<string, string, int> produs = obj as Tuple<string, string, int>;


            if (produs.Item1 != "" && produs.Item2 != "")
            {
                context.AdaugareProdus(produs.Item1, produs.Item2, produs.Item3, true);
            }
        }
        public void AddOferta(object obj)
        {
            Tuple<string, int, int, System.DateTime, System.DateTime> oferta = obj as Tuple<string, int, int, System.DateTime, System.DateTime>;

            // nevoie de verificare ca exista produsul caruia ii facem oferta
            if (oferta.Item1 != "" && oferta.Item3 >=1 && oferta.Item4 != null & oferta.Item5!= null)
            {
                context.AdaugareOferta(oferta.Item1, oferta.Item2, oferta.Item3, oferta.Item4, oferta.Item5);
            }
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
            if (bonFiscal.Item3 >=1 && bonFiscal.Item4 >=1)
            {
                context.AdaugareBonProdus(bonFiscal.Item1, bonFiscal.Item2, bonFiscal.Item3, bonFiscal.Item4);
            }
        }
        public void AddProducator(object obj)
        {
            Tuple<string, string> producator = obj as Tuple<string, string>;

            if (producator.Item1 != "" && producator.Item2 !="")
            {
                context.AdaugareProducator(producator.Item1, producator.Item2);
            }
        }

        internal void AfiseazaDateleProdusuluiVisualizeGrid(string produsNameVisualizeText)
        {
            bool existaProdus = produse2.Any(p=> p.Nume.Trim() == produsNameVisualizeText);

            if (existaProdus)
            {
                Produs prod = produse2.First(p => p.Nume == produsNameVisualizeText);
                string message = "";
                message +="Numele produsului: "+  prod.Nume + "\n"+ "ID Producator: "+ prod.Producator_Id + "\n"+"Activ: "+ prod.IsActive + "\n" +"Id Produs:"+ prod.Id;




                MessageBox.Show(message);
            }
            else
                MessageBox.Show("Nu exista produsul");

        }

        internal void AfiseazaDateleProducatoruluiVisualizeGrid(string producatorNameVisualizeText)
        {
            bool existaProducator = listaProducatori.Contains(producatorNameVisualizeText);

            if(existaProducator) 
            {
                var producatori = context.GetProducatori();

                string data = "";
                foreach(var producator in producatori)
                {
                    if(producator.NumeProducator.Trim() == producatorNameVisualizeText)
                    {
                        data = "Numele producatorului: " + producator.NumeProducator + "\n" + "Id-ul producatorului: " +
                               producator.Id + "\n" + "Tara de origine: " + producator.TaraDeOrigine + "\n";
                        break;
                    }

                }
                MessageBox.Show(data);
            }
            else
                MessageBox.Show("Nu exista producatorul");
        }

        internal void AfiseazaDateleUtilizatorului(string utilizatorNameVisualizeText)
        {
            var listaUtil = context.SelectUtilizatori();

            foreach(var utilizator in listaUtil)
            {
                if(utilizator.Nume.Trim() == utilizatorNameVisualizeText)
                {
                    string data = "Nume Utilizator: " + utilizator.Nume.Trim() + "\n" + "Id: " + utilizator.IdUtilizator + "\n" +
                        "Tip: " + utilizator.Tip.Trim() + "\n" + "Parola: " + utilizator.Parola.Trim() + "\n";

                    MessageBox.Show(data);

                    return;
                }


            }

            MessageBox.Show("Nu exista utilizatorul.");
        }

        internal void AfisareDateBonFiscal(int idBonFiscal)
        {
            var listaBonuriFiscale = context.GetBonuriFiscale();

            foreach(var bonFiscal in listaBonuriFiscale)
            {
                if(bonFiscal.IdBon == idBonFiscal)
                {
                    string data = "IdBon: " + bonFiscal.IdBon + "\n" + "Data eliberare: " + bonFiscal.DataEliberare + "\n" +
                        "Suma incasata: " + bonFiscal.SumaIncasata + "\n" + "Id casier: " + bonFiscal.IdCasier + "\n" +
                        "Cu Produsele: " + "\n";

                    var listaBonProdus = context.GetBonProdus();
                    foreach(var bonProdus in listaBonProdus)
                    {
                        if(bonProdus.IdBon == bonFiscal.IdBon)
                        {
                            data += GetNumeProdusFromId(bonProdus.IdProdus) + "..." + bonProdus.Cantitate +
                                "..." + bonProdus.Subtotal + "\n";
                        }
                    }

                    MessageBox.Show(data);
                    return;
                }
            }

            MessageBox.Show("Nu exista un Bon Fiscal cu acel Id");
        }

        internal void AfisareDateStocProdus(int idStocProdus)
        {
            var listaStocProdus = context.GetStocProduse();

            foreach(var stocProdus in listaStocProdus)
            {
                if(stocProdus.IdStocProdus == idStocProdus)
                {
                    string data = "Id: " + stocProdus.IdStocProdus + "\n" + "Nume Produs: " + GetNumeProdusFromId(stocProdus.IdProdus) + "\n" +
                        "Cantitate: " + stocProdus.Cantitate + "\n" + "Data Expirare: " + stocProdus.DataExpirare + "\n" +
                        "Data Aprovizionare: " + stocProdus.DataAprovizionare + "\n" + "Pret achizitie: " + stocProdus.PretAchizitie + "\n" +
                        "Pret Vanzare: " + stocProdus.PretVanzare + "\n" + "IsActive: " + stocProdus.IsActive;

                    MessageBox.Show(data);

                    return;
                }
            }

            MessageBox.Show("Nu exista un StocProdus cu acel Id");
        }
    }
}
