using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Models.BusinessLogic
{
    public class GeneralBLL
    {
        private ShopMngEntities2 context = new ShopMngEntities2();
        public string ErrorMessage { get; set; }







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

            // Iterează prin fiecare element din result și adaugă sumele încasate pentru fiecare zi în listă
            foreach (var item in result)
            {
                // Formatul sumei încasate poate fi modificat în funcție de necesități
                string sumaFormatted = $"{item.SumaIncasata:N2} RON"; // Afișează suma cu două zecimale și adaugă "RON" la final
                sumePeZile.Add($"{item.Ziua}: {sumaFormatted}"); // Adaugă ziua și suma încasată formatată în listă
            }

            // Returnează lista de sume încasate pe zile
            return sumePeZile;
        }
        public List<string> GetProduseDeLaProducatorul(int producatorId)
        {
            //int? producatorId = context.Producatori.FirstOrDefault(p => p.Nume == numeProducator)?.Id;

            var result = context.GetProduseDeLaProducator(producatorId);

            // Parsarea rezultatului și obținerea numelor produselor
            List<string> numeProduse = result.Select(r => r.Nume).ToList();

            return numeProduse;


        }

        public double GetSumaProduseStocCurentCategoria(string categorie)
        {
            //int? producatorId = context.Producatori.FirstOrDefault(p => p.Nume == numeProducator)?.Id;

            var result = context.CalculeazaSumaTotalaProduseCategoriaSpecifica(categorie);

            double sumaTotala = 0;

            // Iterați prin fiecare element din result și adunați cantitatea * prețul de vânzare la suma totală
            foreach (var item in result)
            {
                sumaTotala += item.Cantitate * item.PretVanzare;
                
            }

            // Returnați suma totală calculată
            return sumaTotala;


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


        private double CalculeazaPretFinalProdus(double pretInitial)
        {
            return (pretInitial + pretInitial * 25 / 100);
        }
        public void AddStocProdus(object obj)
        {
            Tuple<int, int, Nullable<DateTime>, Nullable<DateTime>, string, double> stocProdus = obj as Tuple<int, int, Nullable<DateTime>, Nullable<DateTime>, string, double>;

            //var tupleData = (Tuple<int, int, DateTime, DateTime, string, double, double>)obj;

            //// Folosim valorile extrase pentru a construi tuplul
            //Tuple<int, int, DateTime, DateTime, string, double, double> stocProdus =
            //    Tuple.Create(tupleData.Item1, tupleData.Item2, tupleData.Item3, tupleData.Item4, tupleData.Item5, tupleData.Item6, tupleData.Item7);

            double pretFinal = CalculeazaPretFinalProdus(stocProdus.Item6);
            if (stocProdus.Item5 != "")
            {
                context.AdaugareStocProdus(stocProdus.Item1, stocProdus.Item2, stocProdus.Item3, stocProdus.Item4, stocProdus.Item5.ToString(), stocProdus.Item6, pretFinal);
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




    }
}
