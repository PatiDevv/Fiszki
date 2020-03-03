using Fiszki.Domain;
using Fiszki.DTO;
using Fiszki.Repozytorium;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fiszki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiszkiController : ControllerBase
    {
        [HttpPost]
        public IActionResult UtworzFiszke([FromBody]FiszkaDTO fiszka)
        {
            Fiszka fiszka1 = new Fiszka(fiszka.Pytanie, fiszka.Odpowiedz);

            if(RepozytoriumFiszek.BazaFiszek.Any(x => x.Pytanie == fiszka.Pytanie))
            {
                return Forbid($"Fiszka z pytaniem: {fiszka.Pytanie} juz istanieje.");
            }

            RepozytoriumFiszek.BazaFiszek.Add(fiszka1);
            return Ok();
        }

        [HttpGet("lista")]
        public IList<Fiszka> PobierzFiszki()
        {
            var osiemDniOdDzisiaj = DateTime.UtcNow.AddDays(8);

            return RepozytoriumFiszek
                .BazaFiszek
                .Where(fiszka => fiszka.CzasOstatniejOdpowiedzi == null || fiszka.CzasOstatniejOdpowiedzi > osiemDniOdDzisiaj).ToList();
        }

        [HttpPost("weryfikacja")]
        public IActionResult SprawdzFiszke([FromBody]FiszkaDTO fiszka)
        {
            var fiszkaZBazy = RepozytoriumFiszek.BazaFiszek.Where(x => x.Pytanie == fiszka.Pytanie).FirstOrDefault();
            var osiemDniOdDzisiaj = DateTime.UtcNow.AddDays(8);
            if(fiszkaZBazy.CzasOstatniejOdpowiedzi < osiemDniOdDzisiaj)
            {
                var teraz = DateTime.UtcNow;
                var roznicaDni = osiemDniOdDzisiaj - teraz;
                
                return BadRequest($"Za szybko na powtórke, spróbuj ponownie za {roznicaDni.Days} dni.");
            }

            return Ok(fiszkaZBazy.SprawdzOdpowiedz(fiszka.Odpowiedz));
        }
    }
}