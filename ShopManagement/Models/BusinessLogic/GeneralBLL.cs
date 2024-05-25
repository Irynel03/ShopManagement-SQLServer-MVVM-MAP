using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ShopManagement.Models.BusinessLogic
{
    public class GeneralBLL : INotifyPropertyChanged
    {
        public ShopMngEntities2 context = new ShopMngEntities2();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Produs> produse2 { get; set; } = new List<Produs>();
        public List<Producator> producatori { get; set; } = new List<Producator>();
        public List<String> numeProduse { get; set; } = new List<String>();
        public List<String> listaCateg { get; set; } = new List<String>();
        public List<String> listaProducatori { get; set; } = new List<String>();
        public List<String> listaUtilizatori { get; set; } = new List<String>();
        public List<StocProdus> stocuri { get; set; } = new List<StocProdus>();
        public List<BonFiscal> bonuri { get; set; } = new List<BonFiscal>();

        private Tuple<string, string> _prodDeModificat;
        public Tuple<string, string> prodDeModificat
        {
            get => _prodDeModificat;
            set
            {
                if (_prodDeModificat != value)
                {
                    _prodDeModificat = value;
                    OnPropertyChanged();
                }
            }
        }
        private Tuple<int, string, string, string> _utilDeModificat;
        public Tuple<int, string, string, string> utilDeModificat
        {
            get => _utilDeModificat;
            set
            {
                if (_utilDeModificat != value)
                {
                    _utilDeModificat = value;
                    OnPropertyChanged();
                }
            }
        }
        private Tuple<int, int, string, float> _stocProdusDeModificat;
        public Tuple<int, int, string, float> stocProdusDeModificat
        {
            get => _stocProdusDeModificat;
            set
            {
                if (_stocProdusDeModificat != value)
                {
                    _stocProdusDeModificat = value;
                    OnPropertyChanged();
                }
            }
        }
        private Tuple<int, string, string, int> _produsDeModificat;
        public Tuple<int, string, string, int> produsDeModificat
        {
            get => _produsDeModificat;
            set
            {
                if (_produsDeModificat != value)
                {
                    _produsDeModificat = value;
                    OnPropertyChanged();
                }
            }
        }





        public GeneralBLL()
        {
            VerrificareStocuri();
            SetareActivitateProduseDupaStoc();

            var produse = context.GetProduse1();

            produse2 = GetProduse();
            producatori = GetProducatori();
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

            
            context.UpdateStocProdusIsActiveOnConditions();
            
            List<Tuple<string, int>> listaProd = GetListaProducatori();
            foreach (var prod in listaProd)
                listaProducatori.Add(prod.Item1);

            stocuri = GetStocuri();
            bonuri = GetBonuri();

        }

        private List<BonFiscal> GetBonuri()
        {
            List<BonFiscal> bonuriS = new List<BonFiscal>();
            var bonuriDB = context.GetBonuriFiscale();
            foreach(var b in bonuriDB)
            {
                bonuriS.Add(new BonFiscal(b.IdBon, b.IdCasier, b.SumaIncasata, b.DataEliberare));
            }
            return bonuriS;
        }

        private List<StocProdus> GetStocuri()
        {
            List<StocProdus> stocuriS = new List<StocProdus>();
            var stocuriDB = context.GetStocProduse();
            foreach(var s in stocuriDB)
            {
                stocuriS.Add(new StocProdus(s.IdStocProdus, s.IdProdus, s.Cantitate, s.UnitateMasura, s.DataAprovizionare, s.DataExpirare, s.PretAchizitie, s.PretVanzare));
            }

            return stocuriS;
        }

        private void SetareActivitateProduseDupaStoc()
        {
            List<string> produseDeSetatActive = new List<string>();
            var stocProduse = context.GetStocProduse();
            foreach (var stocProdus in stocProduse)
            {
                bool isActive = (bool)context.GetStocProdusActivity(stocProdus.IdStocProdus).FirstOrDefault();
                string numeProdus = GetNumeProdusFromId(stocProdus.IdProdus);
                if (isActive)
                    produseDeSetatActive.Add(numeProdus);
            }
            SetareActiveDinLista(produseDeSetatActive);
        }

        private void SetareActiveDinLista(List<string> produseDeSetatActive)
        {
            foreach (string produsNume in produseDeSetatActive)
                context.SetProdusActivity(produsNume, true);
        }

        private List<Producator> GetProducatori()
        {
            var producatoriDB = context.GetProducatori();
            List<Producator> producatoriF = new List<Producator>();

            foreach(var producator in producatoriDB)
            {
                producatoriF.Add(new Producator(producator.NumeProducator, producator.TaraDeOrigine, producator.Id, (bool)context.GetProducatorActivity(producator.Id).FirstOrDefault()));
            }

            return producatoriF;
        }

        private void VerrificareStocuri()
        {
            var stocProduse = context.GetStocProduse();
            foreach (var stocProdus in stocProduse)
            {
                if(stocProdus.DataExpirare < DateTime.Now && context.GetStocProdusActivity(stocProdus.IdStocProdus).FirstOrDefault() == true)
                    context.SetStocProdusActivity(stocProdus.IdStocProdus, false);

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
                stocProduse.Add(new StocProdus(item.IdStocProdus, item.IdProdus, item.Cantitate, item.UnitateMasura.Trim(), item.DataAprovizionare,
                    item.DataExpirare,  item.PretAchizitie, item.PretVanzare));
            }

            return stocProduse;
        }


        public List<Produs> GetProduse()
        {
            var result = context.GetProduse1();
            List<Produs> produse = new List<Produs>();

            foreach (var item in result)
            {
                produse.Add(new Produs(item.Id, item.Nume.Trim(), item.Categorie, item.Producator_Id, item.IsActive, item.CodDeBare));
            }

            return produse;
        }

        public List<Tuple<string, string, string, int, bool>> GetUtilizatoriData()
        {
            var result = context.SelectUtilizatori();

            List<Tuple<string, string, string, int, bool>> dateUtilizatori = new List<Tuple<string, string, string, int, bool>>();

            foreach(var item in result)
            {
                dateUtilizatori.Add(Tuple.Create(item.Nume.Trim(), item.Parola.Trim(), item.Tip.Trim(), item.IdUtilizator, (bool)context.GetUtilizatorActivity(item.IdUtilizator).FirstOrDefault()));
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
        public List<string> GetSumePeZileTimpDeOLuna2(string numeCasier, int luna)
        {
            int idCasier = GetUtilIdFromNume(numeCasier);
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

            var produse = context.GetProduse1();
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
                return;
        }

        private int GetIdProdusFromNume(string numeProd)
        {
            var produse = context.GetProduse1();
            foreach (var item in produse)
            {
                if (item.Nume.Trim() == numeProd)
                    return item.Id;
            }
            return -1;
        }
        private string GetNumeProdusFromId(int id)
        {
            var produse3 = context.GetProduse1();
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
                
                MessageBox.Show("Stocul a fost adaugat.");
            }
        }
        public void AddProdus(object obj)
        {
            //Tuple<string, string, int> produs = obj as Tuple<string, string, int>;
            Tuple<string, string, string> produs = obj as Tuple<string, string, string>;
            int idProducator = -1;

            foreach(Producator p in producatori)
                if(p.NumeProducator.Trim() == produs.Item3)
                {
                    idProducator = p.Id;
                    break;
                }

            if(idProducator == -1)
            {
                MessageBox.Show("Producatorul nu exista.");
                return;
            }
            if (produs.Item1 != "" && produs.Item2 != "")
            {
                context.AdaugareProdus(produs.Item1, produs.Item2, idProducator, true);
                MessageBox.Show("Produsul a fost adaugat cu succes.");
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
                               producator.Id + "\n" + "Tara de origine: " + producator.TaraDeOrigine + "\n" + "IsActive: " +
                               (bool)context.GetProducatorActivity(producator.Id).FirstOrDefault();
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
                        "Tip: " + utilizator.Tip.Trim() + "\n" + "Parola: " + utilizator.Parola.Trim() + "\n" +
                        "IsActive: " + (bool)context.GetUtilizatorActivity(utilizator.IdUtilizator).FirstOrDefault();

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
                            data += GetNumeProdusFromId(bonProdus.IdProdus) + " ... " + bonProdus.Cantitate +
                                " ... " + bonProdus.Subtotal + "\n";
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
                        "Pret Vanzare: " + stocProdus.PretVanzare + "\n" + "IsActive: " + context.GetStocProdusActivity(stocProdus.IdStocProdus).FirstOrDefault();

                    MessageBox.Show(data);

                    return;
                }
            }

            MessageBox.Show("Nu exista un StocProdus cu acel Id");
        }

        internal void ModificaActivitateaProdusului(string produsNameVisualizeText)
        {
            foreach (var produs in produse2)
            {
                if (produs.Nume.Trim() == produsNameVisualizeText)
                {
                    // Inversați valoarea actuală a activității
                    bool newActivity = !produs.IsActive;

                    // Actualizați activitatea produsului în baza de date
                    context.UpdateStocProdusIsActiveOnConditions();
                    context.SetProdusActivity(produs.Nume, newActivity);
                    context.SaveChanges(); // Salvați modificările în baza de date
                    produse2 = GetProduse();
                    MessageBox.Show("S-a modificat cu succes");
                    return;
                }
            }

            // Dacă produsul nu a fost găsit, afișați un mesaj
            MessageBox.Show("Produsul nu a fost găsit");
        }


        internal void ModificaActivitateaProducatorului(string numeProd)
        {
            foreach(var producator in producatori)
            {
                if(producator.NumeProducator.Trim() == numeProd)
                {
                    bool currentActivity = (bool)context.GetProducatorActivity(producator.Id).FirstOrDefault();
                    if (currentActivity)
                    {
                        context.SetProducatorActivityToFalseOnCascade(producator.Id);
                        produse2 = GetProduse();
                    }
                    else
                        context.SetProducatorActivity(producator.NumeProducator, !currentActivity);

                    producatori = GetProducatori();
                    MessageBox.Show("S-a modificat cu succes.");
                    return;
                }
            }
            MessageBox.Show("Nu am gasit producatorul.");
        
        }

        internal void SetProducatorPentruModificare(string producatorNameVisualizeText)
        {
            foreach (var producator in producatori)
            {
                if (producator.NumeProducator.Trim() == producatorNameVisualizeText)
                {
                    prodDeModificat = new Tuple<string, string>(producator.NumeProducator.Trim(), producator.TaraDeOrigine.Trim());
                    return;
                }
            }
            MessageBox.Show("Nu s-a gasit producatorul.");
        }

        internal void ModifyProducator(string numeNou, string taraNoua)
        {
            foreach (var producator in producatori)
            {
                if (producator.NumeProducator.Trim() == prodDeModificat.Item1)
                {
                    context.UpdateProducator(producator.Id, numeNou, taraNoua);
                    MessageBox.Show("Sa facut cu succes");
                    producatori = GetProducatori();
                    return;
                }
            }
            MessageBox.Show("Nu s-a gasit");
        }

        internal void ModifyUtilizator(string nume, string tip, string parola)
        {
            if (tip == "Casier" || tip == "Admin")
            {
                context.UpdateUtilizator(utilDeModificat.Item1, nume, parola, tip);
                MessageBox.Show("Modificari facute");
            }
            else
                MessageBox.Show("Tipul introdus nu este corect");    
        }

        internal void SetUtilizatorPentruModificari(string numeUtilizatorModifyText)
        {
            var utilizatori = context.SelectUtilizatori();
            foreach(var u in utilizatori)
            {
                if(u.Nume.Trim() == numeUtilizatorModifyText)
                {
                    utilDeModificat = new Tuple<int, string, string, string>(u.IdUtilizator, "Nume: "+ u.Nume, "Parola: "+ u.Parola,"Tip: "+ u.Tip);
                    return;
                }
            }
            MessageBox.Show("Nu s-a gasit utilizatorul");
        }

        internal void SetStocProdusToModify(string stocProdusIdVisualizeText)
        {
            int idStc = System.Convert.ToInt32(stocProdusIdVisualizeText);
            var stc = context.GetStocProduse();
            foreach(var s in stc)
            {
                if(s.IdStocProdus == idStc)
                {
                    stocProdusDeModificat = new Tuple<int, int, string, float>(s.IdStocProdus, s.Cantitate, s.UnitateMasura, (float)s.PretVanzare);
                    return;
                }
            }
            MessageBox.Show("Nu s-a gasit stocul");
        }
        public double ConvertStringToDouble(string pretVanzareString)
        {
            double pretVanzareDouble;

            if (Double.TryParse(pretVanzareString, out pretVanzareDouble))
            {
                return pretVanzareDouble;
            }
            else
            {
                // Gestionare eroare sau returnare valoare implicită
                throw new ArgumentException("Conversia a eșuat: Format invalid.", nameof(pretVanzareString));
            }
        }
        internal void ModifyStocProdus(string cantitate, string unitateMasura, string pretVanzare)
        {
            context.UpdateStocProdus(stocProdusDeModificat.Item1, System.Convert.ToInt32(cantitate), ConvertStringToDouble(pretVanzare));
            MessageBox.Show("S-a modificat cu succes");
            // de modificat in baza de date procedura incat sa modificam si unitatea de masura
        }

        internal void SetProdusToModify(string produsNameVisualizeText)
        {
            foreach(var p in produse2)
            {
                if(p.Nume.Trim() == produsNameVisualizeText)
                {
                    produsDeModificat = new Tuple<int, string, string, int>(p.Id, p.Nume, p.Categorie, p.Producator_Id);
                    return;
                }
            }
        }

        internal void ModifyProdus(string nume, string categorie, string producatorId)
        {
            context.UpdateProdus(produsDeModificat.Item1, nume, categorie, System.Convert.ToInt32(producatorId));
            MessageBox.Show("S-a modificat cu succes");
        }

        private bool VerificareExistaStocProdus(int id)
        {
            var stocuriProduse = context.GetStocProduse();
            foreach (var s in stocuriProduse)
                if (s.IdStocProdus == id)
                    return true;
            return false;
        }
        internal void ModifyActivityStocProdus(string stocProdusId)
        {
            int id;
            if (int.TryParse(stocProdusId, out id))
            {
                if (VerificareExistaStocProdus(id))
                {       
                    context.SetStocProdusActivity(id, !(bool)context.GetStocProdusActivity(id).FirstOrDefault());

                    MessageBox.Show("Modificarea s-a facut cu succes");
                }
                else
                MessageBox.Show("Nu exista un stoc cu acest id");
            }
            else
                MessageBox.Show("Id-ul trebuie sa fie un numar");

                
        }
        private int GetUtilIdFromNume(string nume)
        {
            var utilizatori = context.SelectUtilizatori();
            foreach(var u in utilizatori)
            {
                if(u.Nume.Trim() == nume)
                {
                    return u.IdUtilizator;
                }
            }
            return -1;
        }
        private string GetUtilNumeFromId(int id)
        {
            var utilizatori = context.SelectUtilizatori();
            foreach (var u in utilizatori)
            {
                if (u.IdUtilizator == id)
                {
                    return u.Nume;
                }
            }
            return "";
        }

        internal void ModifyUtilizatorActivity(string numeUtil)
        {
            int id = GetUtilIdFromNume(numeUtil);
            bool existaUtilizator = VerificaExistentaUtilizator(id);
            if (existaUtilizator)
            {
                context.SetUilizatorActivity(numeUtil, !context.GetUtilizatorActivity(id).FirstOrDefault());
                MessageBox.Show("S-a modificat cu succes");
            }
            else
                MessageBox.Show("Nu s-a gasit utilizatorul");
        }

        private bool VerificaExistentaUtilizator(int id)
        {
            var utilizatori = context.SelectUtilizatori();
            foreach (var u in utilizatori)
            {
                if (u.IdUtilizator == id)
                    return true;
            }

            return false;
        }

        internal void AfiseazaCMMBonDinZiua(DateTime ziBonFiltrare)
        {
            var bonuriFiscale = context.GetBonuriFiscale();
            double sumaMaxima = 0;
            int id = -1, idCasier = 0;
            foreach (var b in bonuriFiscale)
            {
                if (b.DataEliberare == ziBonFiltrare && b.SumaIncasata > sumaMaxima)
                {
                    idCasier = b.IdCasier;
                    id = b.IdBon;
                    sumaMaxima = b.SumaIncasata;
                }
            }

            if (id != -1)
                MessageBox.Show("Id Bon: " + id + "\n\n" + "Nume Casier: " + GetUtilNumeFromId(idCasier) + "\n\n" +
                    "Suma incasata: " + sumaMaxima);
            else
                MessageBox.Show("Nu exista un bon in acea zi");
        
        }
    }
}
