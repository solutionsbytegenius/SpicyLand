namespace SpicyLand.Models
{
    public class Panino
    {
        public Guid PaninoID { get; set; }
        public  string Nome { get; set; }
        public  string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public DateTime DataCreazione { get; set; }
        public  Byte[] Immagine { get; set; }
        public  string PathImage { get; set; }
        public bool InMenu { get; set; }
        public bool PaninoMese { get; set; }

        public Panino()
        {
            PaninoID = Guid.NewGuid();
            Nome = "";
            Descrizione = "";
            Prezzo = 0.0;
            DataCreazione = DateTime.Now;
            Immagine = new byte[0];
            PathImage = "";
            InMenu = false;
            PaninoMese = false;
        }
    }
}
