using Fiszki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiszki.Repozytorium
{
    public class RepozytoriumFiszek
    {
        public static IList<Fiszka> BazaFiszek = new List<Fiszka>
        {
              new Fiszka("zmienna", "variable"),
              new Fiszka("zamiast", "instead"),
              new Fiszka("chwycic", "garb"),
        };
    }
}
