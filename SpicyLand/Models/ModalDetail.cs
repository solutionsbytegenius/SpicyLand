using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpicyLand.Models
{
    public class ModalDetail
    {
        public required string Nome { get; set; }
        public required string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public required string PathImage { get; set; }

    }
}
