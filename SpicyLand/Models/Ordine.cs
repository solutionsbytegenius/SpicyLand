namespace SpicyLand.Models
{
	public class Ordine
	{
		public Guid OrdineID { get; set; }
		public string Cliente { get; set; } = "";
		public string Panino { get; set; } = "";
		public int Quantita { get; set; } = 0;
		public bool Consegnato { get; set; } = false;
		public bool Annullato { get; set; } = false;
		public int numOrdine {  get; set; } = 0;
		public string PlusPatatine { get; set; } = "";
		public string PlusBevanda { get; set; } = "";
		public string Note { get; set; } = "";
	}
}
