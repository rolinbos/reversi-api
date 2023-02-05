using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReversiApi.Data;
using ReversiApi.Enums;
using ReversiApi.Models;
using ReversiApi.Responses;

namespace ReversiApi.Controllers;

[ApiController]
[Route("api/spel")]
public class SpelController
{
    private readonly ApiDbContext _context;
    
    public SpelController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet("openstaand")]
    public IEnumerable<Spel> Openstaand()
    {
        return _context.Spels.Where(spel => spel.Speler2Token == null || spel.Speler2Token == "" && spel.AandeBeurt != Kleur.Geen).ToList();
    }
    
    [HttpGet("spellen-speler")]
    public IEnumerable<Spel> SpellenSpeler(string spelerToken)
    {
        return _context.Spels.Where(
            spel =>
                spel.Speler2Token == spelerToken || spel.Speler1Token == spelerToken 
                && spel.AandeBeurt != Kleur.Geen 
                && spel.Speler1Token != null || spel.Speler2Token != null || spel.Speler1Token != "" || spel.Speler2Token != "").ToList();
    }
    
    [HttpGet("van-gebruiker")]
    public IEnumerable<Spel> VanGebruiker(string Guuid)
    {
        return _context.Spels.Where(spel => spel.Speler1Token == Guuid || spel.Speler2Token == Guuid).ToList();
    }

    [HttpGet("done")]
    public string Done(string token, string spelerToken)
    {
        Spel spel = _context.Spels.Where(spel => spel.Token == token).First();

        if (spel == null)
        {
            return "null";
        }
        
        Kleur winnendeKleur = spel.OverwegendeKleur();

        if (spel.Speler1Token == spelerToken)
        {
            // Speler1Read op true;
        }

        if (spel.Speler2Token == spelerToken)
        {
            // Speler2Read op true;
        }

        // if (spel.speler1Read && spel.speler2Read)
        // {
            // _context.Spels.Remove(spel);
        // }
        _context.SaveChanges();
        
        
        if (winnendeKleur == Kleur.Wit && spel.Speler2Token == spelerToken)
        {
            return "gewonnen";
        } 
        
        if (winnendeKleur == Kleur.Zwart && spel.Speler1Token == spelerToken)
        {
            return "gewonnen";
        }
        
        
        return "gelijk-spel";


        //     DoneResponse doneResponse = new DoneResponse()
        // {
        //     Speler1 = spel.Speler1Token,
        //     Speler2 = spel.Speler2Token,
        // };
        //
        // if (winnendeKleur == Kleur.Wit)
        // {
        //     doneResponse.Speler2Gewonnen = true;
        // } else if (winnendeKleur == Kleur.Zwart)
        // {
        //     doneResponse.Speler1Gewonnen = true;
        // }
        // else
        // {
        //     doneResponse.GelijkSpel = true;
        // }
        //
        // _context.Spels.Remove(spel);
        // _context.SaveChanges();
        //
        // return doneResponse;
    }
    
    [HttpGet("krijg-spel")]
    public ActionResult<Spel> KrijgSpel(string token)
    {
        return _context.Spels.First(spel => spel.Token == token && spel.AandeBeurt != Kleur.Geen);
    }
    
    [HttpPost("join")]
    public ActionResult<Spel> Join([FromForm] string token, [FromForm] string spelerToken)
    {
        Spel spel =  _context.Spels.First(spel => spel.Token == token);
        if (spel == null)
        {
            return spel;
        }

        spel.Speler2Token = spelerToken;
        _context.SaveChanges();

        return spel;
    }
    
    [HttpPost("aanmaken")]
    public ActionResult<Spel> Aanmaken([FromForm] string spelerToken, [FromForm] string omschrijving)
    {
        Spel spel = new Spel()
        {
            Token = Guid.NewGuid().ToString(),
            AandeBeurt = Kleur.Zwart,
            Speler1Token = spelerToken,
            Omschrijving = omschrijving
        };
        
        _context.Spels.Add(spel);
        _context.SaveChanges();

        return spel;
    }
    
    [HttpPost("doe-zet")]
    public ActionResult<DoeZetResponse> DoeEenZet([FromForm] string id, [FromForm] string token, [FromForm] int rij, [FromForm] int kolom) {
        if (token == null) return new DoeZetResponse()
        {
            status = 404,
            message = "Token is niet gevuld",
        };

        Spel spel = _context.Spels.First(spel => spel.Token == id);
        
        
        if (spel == null) return new DoeZetResponse()
        {
            status = 404,
            message = "Spel is niet gevonden",
        };

        if (spel.Afgelopen())
        {
            _context.SaveChanges();
            return SpelIsAfgelopenReponse(spel);
        }
        if (spel.AandeBeurt == Kleur.Zwart && spel.Speler1Token != token) return TokenIsNietGelijkAanSpelerResponse();
        if (spel.AandeBeurt == Kleur.Wit && spel.Speler2Token != token) return TokenIsNietGelijkAanSpelerResponse();
        
        if (!spel.DoeZet(rij, kolom)) return new DoeZetResponse()
        {
            status = 602,
            message = "Deze zet kan niet gezet worden",
        };
            
        _context.SaveChanges();
            
        return new DoeZetResponse()
        {
            status = 200,
            message = "OK",
        };;
    }

    [HttpPost("afgelopen")]
    public ActionResult<DoeZetResponse> Afgelopen([FromForm] string id)
    {
        if (id == null) return new DoeZetResponse()
        {
            status = 404,
            message = "Token is niet gevuld",
        };

        Spel spel = _context.Spels.First(spel => spel.Token == id);

        if (spel == null) return new DoeZetResponse()
        {
            status = 404,
            message = "Spel is niet gevonden",
        };

        if (spel.Afgelopen())
        {
            _context.SaveChanges();
            return SpelIsAfgelopenReponse(spel);
        }
        
        return new DoeZetResponse()
        {
            status = 200,
            message = "OK",
        };
    }

    [HttpPost("overslaan")]
    public ActionResult<DoeZetResponse> Overslaan([FromForm] string id, [FromForm] string token) {
        if (token == null) return new DoeZetResponse()
        {
            status = 404,
            message = "Token is niet gevuld",
        };

        Spel spel = _context.Spels.First(spel => spel.Token == id);

        if (spel == null) return new DoeZetResponse()
        {
            status = 404,
            message = "Spel is niet gevonden",
        };

        if (spel.Afgelopen())
        {
            _context.SaveChanges();
            return SpelIsAfgelopenReponse(spel);
        }
        
        if (spel.AandeBeurt == Kleur.Zwart && spel.Speler1Token != token) return TokenIsNietGelijkAanSpelerResponse();
        if (spel.AandeBeurt == Kleur.Wit && spel.Speler2Token != token) return TokenIsNietGelijkAanSpelerResponse();

        if (!spel.Pas()) return new DoeZetResponse()
        {
            status = 606,
            message = "Je kan nog"
        };
        
        _context.SaveChanges();

        return new DoeZetResponse()
        {
            status = 200,
            message = "OK",
        };
    }

    private DoeZetResponse TokenIsNietGelijkAanSpelerResponse()
    {
        return new DoeZetResponse()
        {
            status = 601,
            message = "Het is niet jouw beurt",
        };
    }

    private DoeZetResponse SpelIsAfgelopenReponse(Spel spel)
    {
        return new DoeZetResponse()
        {
            status = 605,
            message = "Spel is over",
            kleur = spel.OverwegendeKleur(),
        };
    }
}