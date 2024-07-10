using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChambreController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public ChambreController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.Chambres.ToList();
            return Ok(Allobj);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbObj = dbcontext.Chambres.Single(c => c.Id == id);
            if (dbObj == null)
                return NotFound();
            else
                return Ok(dbObj);
        }


        [HttpGet("BySociete/{idSociete:int}")]
        public IActionResult GetBySocieteId(int idSociete)
        {
            var chambres = dbcontext.Chambres
                .Where(c => c.IdSociete == idSociete)
                .ToList();

            if (chambres == null || chambres.Count == 0)
                return NotFound();

            return Ok(chambres);
        }
        [HttpPost]
        public IActionResult AddItem(ChambreDto obj)
        {
            var dbobj = new Chambre
            {
                Name = obj.Name,
                IdSociete =obj.IdSociete
            };

            dbcontext.Chambres.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, ChambreDto obj)
        {
            var dbobj = dbcontext.Chambres.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Name = obj.Name;
            dbobj.IdSociete = obj.IdSociete;

            dbcontext.Chambres.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.Chambres.Find(id);
            if (dbobj is null)
                return NotFound();

            dbcontext.Chambres.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }

        [HttpGet("DetailsBySociete/{societeId:int}")]
        public IActionResult GetChambresBySociete(int societeId)
        {
            try
            {
                var chambres = dbcontext.Chambres
                    .Where(c => c.IdSociete == societeId)
                    .Select(c => new
                    {
                        Chambre = c.Name,
                        FournisseursProduits = c.BonEntrees.Select(be => new
                        {
                            Type = "BonEntree",
                            be.Date,
                            be.Qte,
                            NombreCasier = (int?)be.NombreCasier,
                            NbrScasier = (int?)null,
                            Produit = be.Produit.Name,
                            Fournisseur = be.Fournisseur.Name
                        })
                        .Union(c.BonSorties.Select(bs => new
                        {
                            Type = "BonSortie",
                            bs.Date,
                            bs.Qte,
                            NombreCasier = (int?)null,
                            NbrScasier = (int?)bs.NbrScasier,
                            Produit = bs.Produit.Name,
                            Fournisseur = bs.Fournisseur.Name
                        }))
                        .GroupBy(bp => new { bp.Fournisseur, bp.Produit })
                        .Select(group => new
                        {
                            Fournisseur = group.Key.Fournisseur,
                            Produit = group.Key.Produit,
                            Details = group.Select(bp => new
                            {
                                bp.Type,
                                bp.Date,
                                bp.Qte,
                                NombreCasier = bp.Type == "BonEntree" ? bp.NombreCasier : bp.NbrScasier
                            }),
                            StockTotal = group.Sum(bp => bp.Type == "BonEntree" ? bp.Qte : -bp.Qte) 
                        })
                    })
                    .ToList();

                return Ok(chambres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur est survenue lors de la récupération des données.");
            }
        }

        [HttpGet("DetailsBySocieteByChambre")]
        public IActionResult GetChambresBySociete(int? chambreId = null)
        {
            try
            {
                var query = dbcontext.Chambres.AsQueryable();

                if (chambreId.HasValue)
                {
                    query = query.Where(c => c.Id == chambreId);
                }

                var chambres = query
                    .Select(c => new
                    {
                        Chambre = c.Name, 
                        FournisseursProduits = c.BonEntrees.Select(be => new
                        {
                            Type = "BonEntree",
                            be.Date,
                            be.Qte,
                            NombreCasier = (int?)be.NombreCasier,
                            NbrScasier = (int?)null,
                            Produit = be.Produit.Name,
                            Fournisseur = be.Fournisseur.Name
                        })
                        .Union(c.BonSorties.Select(bs => new
                        {
                            Type = "BonSortie",
                            bs.Date,
                            bs.Qte,
                            NombreCasier = (int?)null,
                            NbrScasier = (int?)bs.NbrScasier,
                            Produit = bs.Produit.Name,
                            Fournisseur = bs.Fournisseur.Name
                        }))
                        .GroupBy(bp => new { bp.Fournisseur, bp.Produit })
                        .Select(group => new
                        {
                            Fournisseur = group.Key.Fournisseur,
                            Produit = group.Key.Produit,
                            Details = group.Select(bp => new
                            {
                                bp.Type,
                                bp.Date,
                                bp.Qte,
                                NombreCasier = bp.Type == "BonEntree" ? bp.NombreCasier : bp.NbrScasier
                            }),
                            StockTotal = group.Sum(bp => bp.Type == "BonEntree" ? bp.Qte : -bp.Qte) 
                        })
                    })
                    .ToList();

                return Ok(chambres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur est survenue lors de la récupération des données.");
            }
        }

        [HttpGet("EtatChambre/{societeId:int}")]
        public IActionResult GetEtatChambresBySociete(int societeId, [FromQuery] int? idChambre = null)
        {
            try
            {
                var query = dbcontext.Chambres
                    .Where(c => c.IdSociete == societeId);

                if (idChambre.HasValue)
                {
                    query = query.Where(c => c.Id == idChambre.Value);
                }

                var chambres = query
                    .Select(c => new
                    {
                        Chambre = c.Name,
                        FournisseursProduits = c.BonEntrees.Select(be => new
                        {
                            Type = "BonEntree",
                            be.Qte,
                            Produit = be.Produit.Name,
                            Fournisseur = be.Fournisseur.Name
                        })
                        .Union(c.BonSorties.Select(bs => new
                        {
                            Type = "BonSortie",
                            bs.Qte,
                            Produit = bs.Produit.Name,
                            Fournisseur = bs.Fournisseur.Name
                        }))
                        .GroupBy(bp => new { bp.Fournisseur, bp.Produit })
                        .Select(group => new
                        {
                            Fournisseur = group.Key.Fournisseur,
                            Produit = group.Key.Produit,
                            TotalEntrees = group.Where(bp => bp.Type == "BonEntree").Sum(bp => (int?)bp.Qte) ?? 0,
                            TotalSorties = group.Where(bp => bp.Type == "BonSortie").Sum(bp => (int?)bp.Qte) ?? 0,
                            StockTotal = (group.Where(bp => bp.Type == "BonEntree").Sum(bp => (int?)bp.Qte) ?? 0) -
                                         (group.Where(bp => bp.Type == "BonSortie").Sum(bp => (int?)bp.Qte) ?? 0)
                        })
                    })
                    .ToList();

                return Ok(chambres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur est survenue lors de la récupération des données: {ex.Message}");
            }
        }












    }

}