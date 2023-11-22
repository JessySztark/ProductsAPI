using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EntityFramework;
using WebApplication1.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.DTO;

public class MarqueManager : IDataRepository<Marque>
{
    readonly ProductDBContext? prodDbContext;

    public MarqueManager() { }

    public MarqueManager(ProductDBContext context)
    {
        prodDbContext = context;
    }

    public ActionResult<IEnumerable<Marque>> GetAll()
    {
        return prodDbContext.Marques.ToList();
    }

    public ActionResult<Marque> GetById(int id)
    {
        return prodDbContext.Marques.FirstOrDefault(u => u.MarqueID == id);
    }

    public ActionResult<Marque> GetByString(string nomMarque)
    {
        return prodDbContext.Marques.FirstOrDefault(u => u.NomMarque.ToUpper() == nomMarque.ToUpper());
    }

    public IEnumerable<MarqueDto> GetAllMarqueDto()
    {
        return prodDbContext.Marques
            .Select(p => new MarqueDto
            {
                Id = p.MarqueID,
                Nom = p.NomMarque,
                NbProduits = p.ProduitMarque.Count()

            })
            .ToList();
    }

    public void Add(Marque entity)
    {
        prodDbContext.Marques.Add(entity);
        prodDbContext.SaveChanges();
    }

    public void Update(Marque Marque, Marque entity)
    {
        prodDbContext.Entry(Marque).State = EntityState.Modified;
        Marque.MarqueID = entity.MarqueID;
        Marque.NomMarque = entity.NomMarque;
        Marque.ProduitMarque = entity.ProduitMarque;
        prodDbContext.SaveChanges();
    }

    public void Delete(Marque Marque)
    {
        prodDbContext.Marques.Remove(Marque);
        prodDbContext.SaveChanges();
    }
}