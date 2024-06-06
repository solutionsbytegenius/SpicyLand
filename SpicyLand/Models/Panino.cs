using System.Drawing;

namespace SpicyLand.Models
{
    public class Panino
    {
        public Guid PaninoID { get; set; }
        public string Nome { get; set; } = "";
        public string Descrizione { get; set; } = "";
        public decimal Prezzo { get; set; }
        public DateTime DataCreazione { get; set; }
        public IFormFile? Immagine { get; set; }
        public string PathImage { get; set; } = "";
        public string InMenu { get; set; } = "";
        public string PaninoMese { get; set; } = "";
		public string Categoria { get; set; } = "";

        public bool New {  get; set; } =false;

       
    }
}
