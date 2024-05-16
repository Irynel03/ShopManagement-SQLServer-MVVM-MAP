//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StocProdus
    {
        public int IdStocProdus { get; set; }
        public int IdProdus { get; set; }
        public int Cantitate { get; set; }
        public System.DateTime DataAprovizionare { get; set; }
        public System.DateTime DataExpirare { get; set; }
        public string UnitateMasura { get; set; }
        public double PretAchizitie { get; set; }
        public double PretVanzare { get; set; }

        public StocProdus(int idStocProdus, int idProdus, int cantitate, DateTime dataAprovizionare, DateTime dataExpirare, string unitateMasura, double pretAchizitie, double pretVanzare)
        {
            IdStocProdus = idStocProdus;
            IdProdus = idProdus;
            Cantitate = cantitate;
            DataAprovizionare = dataAprovizionare;
            DataExpirare = dataExpirare;
            UnitateMasura = unitateMasura;
            PretAchizitie = pretAchizitie;
            PretVanzare = pretVanzare;
        }



        public virtual Produs Produs { get; set; }
    }
}
