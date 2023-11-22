using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EntityFramework;
using WebApplication1.Models.Repository;
using WebApplication1.Models.DTO;

public class ProduitManager : IDataRepository<Produit>
{
    readonly ProductDBContext? prodDbContext;
    public ProduitManager() { }
    public ProduitManager(ProductDBContext context)
    {
        prodDbContext = context;
    }
    public ActionResult<IEnumerable<ProduitDto>> GetAllProduitsDto()
    {
        return prodDbContext.Produits.Select(p=> new ProduitDto
            {
            Id = p.ProduitID,
            Nom = p.NomProduit,
            Marque = p.MarqueProduit.NomMarque,
            Type = p.TypeProduit_Produit.NomTypeProduit
            }
        ).ToList();
    }

    public ActionResult<IEnumerable<ProduitDetailDto>> GetAllProduitDetailsDto()
    {
        return prodDbContext.Produits.Select(p => new ProduitDetailDto
        {
            Id = p.ProduitID,
            Nom = p.NomProduit,
            Marque = p.MarqueProduit.NomMarque,
            Type = p.TypeProduit_Produit.NomTypeProduit,
            Description = p.Description,
            NomPhoto = p.NomPhoto,
            Uriphoto = p.UrlPhoto,
            Stock = p.StockReel,
            EnReappro = p.StockReel <= p.StockMin
        }
        ).ToList();
    }

    public ActionResult<Produit> GetById(int id)
    {
        return prodDbContext.Produits.FirstOrDefault(u => u.ProduitID == id);
    }
    public ActionResult<Produit> GetByString(string nomProduit)
    {
        return prodDbContext.Produits.FirstOrDefault(u => u.NomProduit.ToUpper() == nomProduit.ToUpper());
    }
    public void Add(Produit entity)
    {
        prodDbContext.Produits.Add(entity);
        prodDbContext.SaveChanges();
    }
    public void Update(Produit Produit, Produit entity)
    {
        prodDbContext.Entry(Produit).State = EntityState.Modified;
        Produit.ProduitID = entity.ProduitID;
        Produit.NomProduit = entity.NomProduit;
        Produit.Description = entity.Description;
        Produit.NomPhoto = entity.NomPhoto;
        Produit.UrlPhoto = entity.UrlPhoto;
        Produit.MarqueID = entity.MarqueID;
        Produit.MarqueProduit = entity.MarqueProduit;
        Produit.TypeProduitID = entity.TypeProduitID;
        Produit.TypeProduit_Produit = entity.TypeProduit_Produit;
        prodDbContext.SaveChanges();
    }
    public void Delete(Produit Produit)
    {
        prodDbContext.Produits.Remove(Produit);
        prodDbContext.SaveChanges();
    }

    ActionResult<IEnumerable<Produit>> IDataRepository<Produit>.GetAll()
    {
        return prodDbContext.Produits.ToList();
    }
}