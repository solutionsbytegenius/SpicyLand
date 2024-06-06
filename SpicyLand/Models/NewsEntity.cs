using Microsoft.EntityFrameworkCore;

namespace SpicyLand.Models
{
    [Keyless]
    public class NewsEntity
    {
        public Guid NewsID { get; set; }
        public string TitoloNotizia { get; set; }
        public string CorpoNotizia { get; set; }
        public string Occhiello { get; set; }
        public DateTime DataInserimento { get; set; }
        public bool Visibile { get; set; }
        public string ImmaginePath { get; set; }
        public DateTime? ScadenzaNotizia { get; set; }
        public bool Scaduta {  get; set; }
        public bool InPrimoPiano {  get; set; }
        public byte[] Immagine { get; set;}
    }
}
