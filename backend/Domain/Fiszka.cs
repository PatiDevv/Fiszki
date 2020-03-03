using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiszki.Domain
{
    public class Fiszka
    {
        public string Pytanie { get; private set; }

        public string Odpowiedz { get; private set; }

        public int IloscPoprawnychOdpowiedzi { get; set; }

        public DateTime? CzasOstatniejOdpowiedzi { get; set; }

        protected Fiszka()
        {
        }

        public Fiszka(string pytanie, string odpowiedz)
        {
            this.Pytanie = pytanie;
            this.Odpowiedz = odpowiedz;
        }

        public bool SprawdzOdpowiedz(string odpowiedz)
        {
            CzasOstatniejOdpowiedzi = DateTime.UtcNow;
            if (odpowiedz == Odpowiedz)
            {
                IloscPoprawnychOdpowiedzi++;
                return true;
            }
            if(IloscPoprawnychOdpowiedzi > 0)
            {
                IloscPoprawnychOdpowiedzi--;
            }

            return false;
        }
    }
}
