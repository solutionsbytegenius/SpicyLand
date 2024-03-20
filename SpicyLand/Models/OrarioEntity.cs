using Microsoft.EntityFrameworkCore;

namespace SpicyLand.Models
{
    [Keyless]
    public class OrarioEntity
    {
        public Guid OrarioID { get; set; }
        public bool Chiuso { get; set; }
        public required string Giorno { get; set; }
        public int NumeroGiorno { get; set; }
        public string? OrarioInizio { get; set; }
        public string? OrarioFine { get; set; }
        public bool Attivo { get; set; }
        
    }
}
