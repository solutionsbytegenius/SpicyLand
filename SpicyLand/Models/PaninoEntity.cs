using Microsoft.EntityFrameworkCore;

namespace SpicyLand.Models
{
    [Keyless]
    public class PaninoEntity
    {
        public Guid PaninoID { get; set; }
        public string Nome { get; set; } = "";
        public string Descrizione { get; set; } = "";
        public decimal Prezzo { get; set;}
        public DateTime DataCreazione { get; set;}
        public string PathImage { get; set; } = "";
        public bool InMenu { get; set;}
        public bool PaninoMese { get; set;}
        public string Categoria { get; set; } = "";
    }
}
