namespace SpicyLand.Models
{
    public class CartItem
    {
        #region Vars
        public Guid ID { get; set; }
        public Guid PaninoID { get; set; }
        public string Panino { get; set; } = "";
        public bool Plus { get; set; } = false;
        public string Note { get; set; } = "";
        public string Bevanda { get; set; } = "";
        public double Prezzo { get; set; }

        #endregion


        #region Costruttori
        /// <summary>
        /// Costruttore nullo
        /// </summary>
        public CartItem()
        {
            ID = new Guid();
            PaninoID = new Guid();
            Plus = false;
            Panino = "";
            Note = "";
			Bevanda = "";
            Prezzo = 0;
        }
        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="id"></param>
        /// <param name="panino"></param>
        /// <param name="plus"></param>
        /// <param name="note"></param>
        /// <param name="bevanda"></param>
        public CartItem( Guid panino, bool plus, string note, string bevanda)
        {
            ID = Guid.NewGuid();
            PaninoID = panino;
            Plus = plus;
            Note = note;
            Bevanda = bevanda;
        }
        public CartItem(Guid id)
        {
            ID = id;
			PaninoID = new Guid();
			Plus = false;
			Panino = "";
			Note = "";
			Bevanda = "";
			Prezzo = 0;
		}
        #endregion
    }
}
