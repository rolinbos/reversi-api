using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReversiApi.Data;
using ReversiApi.Enums;
using ReversiApi.Models;

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
        return _context.Spels.Where(spel => spel.Speler2Token == null || spel.Speler2Token == "").ToList();
    }
    
    [HttpGet("spellen-speler")]
    public IEnumerable<Spel> SpellenSpeler(string spelerToken)
    {
        return _context.Spels.Where(spel => spel.Speler2Token == spelerToken || spel.Speler1Token == spelerToken && spel.AandeBeurt != Kleur.Geen && spel.Speler1Token != null || spel.Speler2Token != null || spel.Speler1Token != "" || spel.Speler2Token != "").ToList();
    }
    
    [HttpGet("van-gebruiker")]
    public IEnumerable<Spel> VanGebruiker(string Guuid)
    {
        return _context.Spels.Where(spel => spel.Speler1Token == Guuid || spel.Speler2Token == Guuid).ToList();
    }

    [HttpGet("krijg-spel")]
    public ActionResult<Spel> KrijgSpel(string token)
    {
        return _context.Spels.First(spel => spel.Token == token);
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
    public ActionResult<string> DoeEenZet([FromForm] string id, [FromForm] string token, [FromForm] int rij, [FromForm] int kolom) {
        if (token == null) return null;

        Spel spel = _context.Spels.First(spel => spel.Token == id);
        
        
        if (spel == null) return null;
        if (spel.AandeBeurt == Kleur.Geen) return "GAMEOVER";
        
        if (spel.AandeBeurt == Kleur.Zwart)
        {
            if (spel.Speler1Token != token) return "NOTYOURTURN";
        }
        
        if (spel.AandeBeurt == Kleur.Wit)
        {
            if (spel.Speler2Token != token) return "NOTYOURTURN";
        }
        
        if (!spel.DoeZet(rij, kolom)) return "IMPOSSIBLE";
            
        _context.SaveChanges();
            
        return "OK";
    }
}