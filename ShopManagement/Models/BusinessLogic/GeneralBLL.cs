using System;

namespace ShopManagement.Models.BusinessLogic
{
    public class GeneralBLL
    {
        private ShopMngEntities2 context = new ShopMngEntities2();
        public string ErrorMessage { get; set; }

        public void AddUtilizator(object obj)
        {
            Tuple<string, string, string> utilizator = obj as Tuple<string, string, string>;

            if (utilizator.Item1 != "" && utilizator.Item2 != "" && (utilizator.Item3 == "admin" || utilizator.Item3 == "casier"))
            {
                context.AdaugareUtilizator(utilizator.Item1, utilizator.Item2, utilizator.Item3);
            }
            else
                ErrorMessage = "Date introduse gresit la utilizator";
        }
        public void AddStocProdus(object obj)
        {
            Tuple<int, int, Nullable<DateTime>, Nullable<DateTime>, string, double, double> stocProdus = obj as Tuple<int, int, Nullable<DateTime>, Nullable<DateTime>, string, double, double>;

            //var tupleData = (Tuple<int, int, DateTime, DateTime, string, double, double>)obj;

            //// Folosim valorile extrase pentru a construi tuplul
            //Tuple<int, int, DateTime, DateTime, string, double, double> stocProdus =
            //    Tuple.Create(tupleData.Item1, tupleData.Item2, tupleData.Item3, tupleData.Item4, tupleData.Item5, tupleData.Item6, tupleData.Item7);

            if (stocProdus.Item5 != "")
            {
                context.AdaugareStocProdus(stocProdus.Item1, stocProdus.Item2, stocProdus.Item3, stocProdus.Item4, stocProdus.Item5.ToString(), stocProdus.Item6, stocProdus.Item7);
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
