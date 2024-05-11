﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ShopMngEntities2 : DbContext
    {
        public ShopMngEntities2()
            : base("name=ShopMngEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BonFiscal> BonFiscal { get; set; }
        public virtual DbSet<BonProdus> BonProdus { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Producator> Producator { get; set; }
        public virtual DbSet<Produs> Produs { get; set; }
        public virtual DbSet<StocProdus> StocProdus { get; set; }
        public virtual DbSet<Utilizator> Utilizator { get; set; }
    
        public virtual int AdaugareBonFiscal(Nullable<int> idCasier, Nullable<double> sumaIncasata)
        {
            var idCasierParameter = idCasier.HasValue ?
                new ObjectParameter("IdCasier", idCasier) :
                new ObjectParameter("IdCasier", typeof(int));
    
            var sumaIncasataParameter = sumaIncasata.HasValue ?
                new ObjectParameter("SumaIncasata", sumaIncasata) :
                new ObjectParameter("SumaIncasata", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AdaugareBonFiscal", idCasierParameter, sumaIncasataParameter);
        }
    
        public virtual int AdaugareProducator(string numeProducator, string taraDeOrigine)
        {
            var numeProducatorParameter = numeProducator != null ?
                new ObjectParameter("NumeProducator", numeProducator) :
                new ObjectParameter("NumeProducator", typeof(string));
    
            var taraDeOrigineParameter = taraDeOrigine != null ?
                new ObjectParameter("TaraDeOrigine", taraDeOrigine) :
                new ObjectParameter("TaraDeOrigine", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AdaugareProducator", numeProducatorParameter, taraDeOrigineParameter);
        }
    
        public virtual int AdaugareProdus(string nume, string categorie, string producator_Id)
        {
            var numeParameter = nume != null ?
                new ObjectParameter("Nume", nume) :
                new ObjectParameter("Nume", typeof(string));
    
            var categorieParameter = categorie != null ?
                new ObjectParameter("Categorie", categorie) :
                new ObjectParameter("Categorie", typeof(string));
    
            var producator_IdParameter = producator_Id != null ?
                new ObjectParameter("Producator_Id", producator_Id) :
                new ObjectParameter("Producator_Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AdaugareProdus", numeParameter, categorieParameter, producator_IdParameter);
        }
    
        public virtual int AdaugareUtilizator(string nume, string parola, string tip)
        {
            var numeParameter = nume != null ?
                new ObjectParameter("Nume", nume) :
                new ObjectParameter("Nume", typeof(string));
    
            var parolaParameter = parola != null ?
                new ObjectParameter("Parola", parola) :
                new ObjectParameter("Parola", typeof(string));
    
            var tipParameter = tip != null ?
                new ObjectParameter("Tip", tip) :
                new ObjectParameter("Tip", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AdaugareUtilizator", numeParameter, parolaParameter, tipParameter);
        }
    
        public virtual int AdaugareBonProdus(Nullable<int> idBon, Nullable<int> idProdus, Nullable<int> cantitate, Nullable<double> subtotal)
        {
            var idBonParameter = idBon.HasValue ?
                new ObjectParameter("IdBon", idBon) :
                new ObjectParameter("IdBon", typeof(int));
    
            var idProdusParameter = idProdus.HasValue ?
                new ObjectParameter("IdProdus", idProdus) :
                new ObjectParameter("IdProdus", typeof(int));
    
            var cantitateParameter = cantitate.HasValue ?
                new ObjectParameter("Cantitate", cantitate) :
                new ObjectParameter("Cantitate", typeof(int));
    
            var subtotalParameter = subtotal.HasValue ?
                new ObjectParameter("Subtotal", subtotal) :
                new ObjectParameter("Subtotal", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AdaugareBonProdus", idBonParameter, idProdusParameter, cantitateParameter, subtotalParameter);
        }
    
        public virtual int AdaugareOferta(string motivOferta, Nullable<int> idProdus, Nullable<int> procentReducere, Nullable<System.DateTime> dataInceputOferta, Nullable<System.DateTime> dataFinalOferta)
        {
            var motivOfertaParameter = motivOferta != null ?
                new ObjectParameter("MotivOferta", motivOferta) :
                new ObjectParameter("MotivOferta", typeof(string));
    
            var idProdusParameter = idProdus.HasValue ?
                new ObjectParameter("IdProdus", idProdus) :
                new ObjectParameter("IdProdus", typeof(int));
    
            var procentReducereParameter = procentReducere.HasValue ?
                new ObjectParameter("ProcentReducere", procentReducere) :
                new ObjectParameter("ProcentReducere", typeof(int));
    
            var dataInceputOfertaParameter = dataInceputOferta.HasValue ?
                new ObjectParameter("DataInceputOferta", dataInceputOferta) :
                new ObjectParameter("DataInceputOferta", typeof(System.DateTime));
    
            var dataFinalOfertaParameter = dataFinalOferta.HasValue ?
                new ObjectParameter("DataFinalOferta", dataFinalOferta) :
                new ObjectParameter("DataFinalOferta", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AdaugareOferta", motivOfertaParameter, idProdusParameter, procentReducereParameter, dataInceputOfertaParameter, dataFinalOfertaParameter);
        }
    }
}
