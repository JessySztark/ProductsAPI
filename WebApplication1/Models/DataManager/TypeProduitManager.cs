using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EntityFramework;
using WebApplication1.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.DTO;

public class TypeProduitManager : IDataRepository<TypeProduit>
{
    readonly ProductDBContext? typeprodDbContext;

    public TypeProduitManager() { }

    public TypeProduitManager(ProductDBContext context)
    {
        typeprodDbContext = context;
    }

    public ActionResult<IEnumerable<TypeProduit>> GetAll()
    {
        return typeprodDbContext.TypesProduit.ToList();
    }

    public ActionResult<TypeProduit> GetById(int id)
    {
        return typeprodDbContext.TypesProduit.FirstOrDefault(u => u.TypeProduitID == id);
    }

    public ActionResult<TypeProduit> GetByString(string nomTypeProduit)
    {
        return typeprodDbContext.TypesProduit.FirstOrDefault(u => u.NomTypeProduit.ToUpper() == nomTypeProduit.ToUpper());
    }

    public IEnumerable<TypeProduitDto> GetAllTypeProduitDto()
    {
        return typeprodDbContext.TypesProduit
            .Select(p => new TypeProduitDto
            {
                Id = p.TypeProduitID,
                Nom = p.NomTypeProduit,
                Produit = p.Produit_TypeProduit.ToString()
            })
            .ToList();
    }

    public void Add(TypeProduit entity)
    {
        typeprodDbContext.TypesProduit.Add(entity);
        typeprodDbContext.SaveChanges();
    }

    public void Update(TypeProduit TypeProduit, TypeProduit entity)
    {
        typeprodDbContext.Entry(TypeProduit).State = EntityState.Modified;
        TypeProduit.TypeProduitID = entity.TypeProduitID;
        TypeProduit.NomTypeProduit = entity.NomTypeProduit;
        TypeProduit.Produit_TypeProduit = entity.Produit_TypeProduit;
        typeprodDbContext.SaveChanges();
    }

    public void Delete(TypeProduit TypeProduit)
    {
        typeprodDbContext.TypesProduit.Remove(TypeProduit);
        typeprodDbContext.SaveChanges();
    }
}