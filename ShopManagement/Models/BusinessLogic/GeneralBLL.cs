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
        public void AddProdus(object obj)
        {
            Tuple<string, string, string> utilizator = obj as Tuple<string, string, string>;

            if (utilizator.Item1 != "" && utilizator.Item2 != "" && (utilizator.Item3 == "admin" || utilizator.Item3 == "casier"))
            {
                context.AdaugareProdus(utilizator.Item1, utilizator.Item2, utilizator.Item3);
            }
        }
        public void AddOferta(object obj)
        {
            Tuple<string, string, string> utilizator = obj as Tuple<string, string, string>;

            if (utilizator.Item1 != "" && utilizator.Item2 != "" && (utilizator.Item3 == "admin" || utilizator.Item3 == "casier"))
            {
                context.AdaugareOferta(utilizator.Item1, utilizator.Item2, utilizator.Item3);
            }
        }
        public void AddBonFiscal(object obj)
        {
            Tuple<string, string, string> utilizator = obj as Tuple<string, string, string>;

            if (utilizator.Item1 != "" && utilizator.Item2 != "" && (utilizator.Item3 == "admin" || utilizator.Item3 == "casier"))
            {
                context.AdaugareBonFiscal(utilizator.Item1, utilizator.Item2, utilizator.Item3);
            }
        }
        public void AddBonProdus(object obj)
        {
            Tuple<string, string, string> utilizator = obj as Tuple<string, string, string>;

            if (utilizator.Item1 != "" && utilizator.Item2 != "" && (utilizator.Item3 == "admin" || utilizator.Item3 == "casier"))
            {
                context.AdaugareBonProdus(utilizator.Item1, utilizator.Item2, utilizator.Item3);
            }
        }
        public void AddProducator(object obj)
        {
            Tuple<string, string, string> utilizator = obj as Tuple<string, string, string>;

            if (utilizator.Item1 != "" && utilizator.Item2 != "" && (utilizator.Item3 == "admin" || utilizator.Item3 == "casier"))
            {
                context.AdaugareProducator(utilizator.Item1, utilizator.Item2, utilizator.Item3);
            }
        }




    }
}
