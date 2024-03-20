using Microsoft.EntityFrameworkCore;

namespace SpicyLand.Models
{
    [Keyless]
    public class OrdineEntity
    {

        public Guid OrdineID { get; set; }
        public Guid PaninoID { get; set; }
        public bool Plus { get; set; }
        public float PrezzoFinale { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public bool Consegnato { get; set; }
        public bool InLavorazione { get; set; }
        public bool Annullato { get; set; }
        public DateTime DataAnnullamento { get; set; }
        public string Note { get; set; } = "";
		public string Bevanda { get; set; } = "";
		public string Cliente { get; set; } = "";

	}
}
