namespace SpicyLand.Models
{
    public class Notizie
    {
        public Guid NewsID { get; set; }
        public string TitoloNotizia { get; set; } = string.Empty;
        public string CorpoNotizia { get; set; } = string.Empty;
        public string Occhiello { get; set; } = string.Empty;
        public DateTime DataInserimento { get; set; }
        public string Visibile { get; set; } = string.Empty;
        public string ImmaginePath { get; set; } = string.Empty;
        public DateTime? ScadenzaNotizia { get; set; }
        public string Scaduta { get; set; } = "";
        public string InPrimoPiano { get; set; } = "";
        public bool New {  get; set; }
		public IFormFile Immagine { get; set; }
	}
}
