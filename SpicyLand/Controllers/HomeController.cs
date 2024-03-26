using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using SpicyLand.Data;
using System.Runtime.Serialization.Formatters.Binary;
using SpicyLand.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text.Json;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System;
using Microsoft.Extensions.Hosting;
using System.Drawing;

namespace SpicyLand.Controllers
{
    public class HomeController : Controller
    {
        #region Vars
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private CartItemCollection CartCollection = new CartItemCollection();
        private string connectionString = "Server=tcp:solutionsbyte.database.windows.net,1433;Initial Catalog=SpicyLand;Persist Security Info=False;User ID=dimarco;Password=SistemiCloud2023@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion


        #region methods

        #region modal
        public IActionResult ShowModal(Guid PaninoID)
        {
#pragma warning disable CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
            PaninoEntity Panino = _db.Panino.FirstOrDefault(x => x.PaninoID == PaninoID);
            Panino.PathImage = Panino.PathImage.Substring(7);
#pragma warning restore CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
            return PartialView("ModalDetail", Panino);
        }

        public IActionResult ShowEditModal(string Scelta)
        {
            if (!String.IsNullOrEmpty(Scelta) && Scelta == "Add")
            {
                return PartialView("ModalEditMenu");
            }
            Guid PaninoID = new Guid(Scelta);
#pragma warning disable CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
            PaninoEntity Pan = _db.Panino.FirstOrDefault(x => x.PaninoID == PaninoID);
            Panino panino = new Panino()
            {
                PaninoID = Pan.PaninoID,
                Nome = Pan.Nome,
                Descrizione = Pan.Descrizione,
                Categoria = Pan.Categoria,
                InMenu = Pan.InMenu? "on":"",
                PaninoMese = Pan.PaninoMese? "on":"",
                Prezzo = Pan.Prezzo,
                PathImage = Pan.PathImage
            };
#pragma warning restore CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
            return PartialView("ModalEditMenu", panino);
        }

        public IActionResult ShowOrderModal(int Order)
        {
#pragma warning disable CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
            OrdineEntity ord = _db.Ordine.FirstOrDefault(x => x.NumeroOrdine == Order && x.DataPrenotazione.Date == DateTime.Now.Date);
            if (ord != null)
            {
                string panino = _db.Panino.FirstOrDefault(x => x.PaninoID == ord.PaninoID).Nome;
                Ordine ordine = new Ordine()
                {
                    OrdineID = ord.OrdineID,
                    Panino = panino,
                    PlusBevanda = ord.Bevanda,
                    PlusPatatine = ord.Patatine,
                    Note = ord.Note,
                    Cliente = ord.Cliente,
                    numOrdine = ord.NumeroOrdine
                };
#pragma warning restore CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
                return PartialView("OrderDetail", ordine);
            }
            return RedirectToAction("OrderDetail");
        }
        #endregion

        #region Pages
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Count", "0");
            IEnumerable<NewsEntity> News = _db.News.Where(x => x.ScadenzaNotizia <= DateTime.Now).ToList();
            if (News.Any())
                return View(News);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext?.Session.GetString("UserID")))
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Clear();
            return Redirect("Index");
        }
        public IActionResult Contatti()
        {
            IEnumerable<OrarioEntity> orario = _db.Orario.OrderBy(x => x.NumeroGiorno).ToList();
            return View(orario);
        }
        [HttpGet]
        public IActionResult Menu()
        {
            IEnumerable<PaninoEntity> Panino = _db.Panino.Where(x => x.InMenu == true).ToList();
            foreach (var p in Panino)
            {
                p.PathImage = p.PathImage.Substring(7);
            }
            return View(Panino);
        }

        public IActionResult Ordinazioni()
        {
            OrdineCollection OrdineCollection = new OrdineCollection();
            //
            List<OrdineEntity> Ordini = _db.Ordine.Where(x => x.DataPrenotazione.Date == DateTime.Now.Date && x.Annullato != true).ToList();

            foreach (var ordine in Ordini)
            {
                var item = new Ordine()
                {
                    OrdineID = ordine.OrdineID,
                    Consegnato = false,
                    Annullato = false,
                    Cliente = ordine.Cliente
                };
                if (OrdineCollection != null)
                {
                    var panino = _db.Panino.FirstOrDefault(x => x.PaninoID == ordine.PaninoID);

                    if (OrdineCollection.Any(x => x.Panino == panino.Nome && !String.IsNullOrEmpty(x.PlusBevanda) && !String.IsNullOrEmpty(x.Note) && !String.IsNullOrEmpty(x.PlusPatatine) && x.PlusPatatine == ordine.Patatine && x.PlusBevanda == ordine.Bevanda))
                    {
                        OrdineCollection.FirstOrDefault(x => x.Panino == panino.Nome && !String.IsNullOrEmpty(x.PlusBevanda) && !String.IsNullOrEmpty(x.Note) && !String.IsNullOrEmpty(x.PlusPatatine) && x.PlusPatatine == ordine.Patatine && x.PlusBevanda == ordine.Bevanda).Quantita++;
                    }
                    else
                    {
                        item.Panino = panino.Nome;
                        item.Quantita = 1;
                        if (ordine.Plus)
                        {
                            item.PlusBevanda = ordine.Bevanda;
                            item.PlusPatatine = ordine.Patatine;
                        }
                        item.Note = ordine.Note;
                        item.Consegnato = ordine.Consegnato;
                        item.Annullato = ordine.Annullato;
                        item.numOrdine = OrdineCollection.Count + 1;
                        OrdineCollection.Add(item);
                    }
                }
                else
                {
                    item.numOrdine = 1;
                    OrdineCollection.Add(item);
                }
            }
            if (OrdineCollection != null)
                return View("Ordinazioni", OrdineCollection);
            else return View("Ordinazioni");
        }

        public IActionResult EditMenu()
        {
            IEnumerable<PaninoEntity> Panino = _db.Panino.ToList();

            if (!String.IsNullOrEmpty(HttpContext?.Session.GetString("UserID")))
            {
                return View(Panino);
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult News()
        {
            IEnumerable<NewsEntity> News = _db.News.ToList();

            if (!String.IsNullOrEmpty(HttpContext?.Session.GetString("UserID")))
            {
                return View(News);
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Carrello()
        {
            if (!String.IsNullOrEmpty(HttpContext?.Session.GetString("Collection")))
            {
#pragma warning disable CS8602 // Dereferenziamento di un possibile riferimento Null.
                var jsonStringFromSession = HttpContext.Session.GetString("Collection");
#pragma warning restore CS8602 // Dereferenziamento di un possibile riferimento Null.
#pragma warning disable CS8601 // Possibile assegnazione di riferimento Null.
#pragma warning disable CS8604 // Possibile argomento di riferimento Null.
                CartCollection = JsonConvert.DeserializeObject<CartItemCollection>(jsonStringFromSession);
#pragma warning restore CS8604 // Possibile argomento di riferimento Null.
#pragma warning restore CS8601 // Possibile assegnazione di riferimento Null.
            }
            if (CartCollection.Any())
            {
                return View("Carrello", CartCollection);
            }
            return View("Carrello");
        }
        #endregion

        #region actions
        [HttpPost]
        public IActionResult AddCart(CartItem item)
        {
            double totale = 0;
            if (!String.IsNullOrEmpty(HttpContext?.Session.GetString("Collection")))
            {
#pragma warning disable CS8602 // Dereferenziamento di un possibile riferimento Null.
                var jsonStringFromSession = HttpContext.Session.GetString("Collection");
#pragma warning restore CS8602 // Dereferenziamento di un possibile riferimento Null.
#pragma warning disable CS8601 // Possibile assegnazione di riferimento Null.
#pragma warning disable CS8604 // Possibile argomento di riferimento Null.
                CartCollection = JsonConvert.DeserializeObject<CartItemCollection>(jsonStringFromSession);
#pragma warning restore CS8604 // Possibile argomento di riferimento Null.
#pragma warning restore CS8601 // Possibile assegnazione di riferimento Null.
                totale = double.Parse(HttpContext?.Session.GetString("Totale"));
            }
            item.ID = Guid.NewGuid();
            if (String.IsNullOrEmpty(item.Note)) item.Note = "";
            if (String.IsNullOrEmpty(item.Bevanda)) item.Bevanda = "";
            if (String.IsNullOrEmpty(item.Patatine)) item.Patatine = "";
            if (item.Plus) item.Prezzo += 2.50;
            if (String.IsNullOrEmpty(item.Panino) || item.Panino.ToLower() == "undefined")
            {
                item.Panino = _db.Panino.FirstOrDefault(x => x.PaninoID == item.PaninoID).Nome;
            }

#pragma warning disable CS8602 // Dereferenziamento di un possibile riferimento Null.
            CartCollection.Add(item);

            totale += item.Prezzo;
#pragma warning restore CS8602 // Dereferenziamento di un possibile riferimento Null.

            var jsonString = JsonConvert.SerializeObject(CartCollection);
            HttpContext.Session.SetString("Collection", jsonString.ToString());
            HttpContext.Session.SetString("Totale", totale.ToString());
            HttpContext.Session.SetString("Count", CartCollection.Count.ToString());
            return RedirectToAction("Menu");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(Guid ID)
        {
            double totale = 0;
            if (!String.IsNullOrEmpty(HttpContext?.Session.GetString("Collection")))
            {
#pragma warning disable CS8602 // Dereferenziamento di un possibile riferimento Null.
                var jsonStringFromSession = HttpContext.Session.GetString("Collection");
#pragma warning restore CS8602 // Dereferenziamento di un possibile riferimento Null.
#pragma warning disable CS8601 // Possibile assegnazione di riferimento Null.
#pragma warning disable CS8604 // Possibile argomento di riferimento Null.
                CartCollection = JsonConvert.DeserializeObject<CartItemCollection>(jsonStringFromSession);
#pragma warning restore CS8604 // Possibile argomento di riferimento Null.
#pragma warning restore CS8601 // Possibile assegnazione di riferimento Null.
                totale = double.Parse(HttpContext?.Session.GetString("Totale"));
            }
            CartItem item = CartCollection.FirstOrDefault(x => x.ID == ID);
            totale -= item.Prezzo;
#pragma warning disable CS8602 // Dereferenziamento di un possibile riferimento Null.
            CartCollection.Remove(item);
#pragma warning restore CS8602 // Dereferenziamento di un possibile riferimento Null.
            var jsonString = JsonConvert.SerializeObject(CartCollection);
            if (jsonString == "[]") jsonString = "";
            HttpContext.Session.SetString("Collection", jsonString.ToString());
            HttpContext.Session.SetString("Totale", totale.ToString());
            HttpContext.Session.SetString("Count", CartCollection.Count.ToString());
            return RedirectToAction("Carrello", CartCollection);
        }


        [HttpPost]
        public IActionResult Verify(AccountPipe A)
        {
            IEnumerable<UserEntity> User = _db.User.ToList();

            var UserLog = User.FirstOrDefault(x => x.UserName == A.Username && x.Password == A.Password);
            if (UserLog != null)
            {
                HttpContext.Session.SetString("UserID", UserLog.UserID.ToString());
                HttpContext.Session.SetString("UserName", UserLog.UserName);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult CompleteOrdine(string Cliente)
        {
            float totale = 0;
            if (!String.IsNullOrEmpty(HttpContext?.Session.GetString("Collection")))
            {
#pragma warning disable CS8602 // Dereferenziamento di un possibile riferimento Null.
                var jsonStringFromSession = HttpContext.Session.GetString("Collection");
#pragma warning restore CS8602 // Dereferenziamento di un possibile riferimento Null.
#pragma warning disable CS8601 // Possibile assegnazione di riferimento Null.
#pragma warning disable CS8604 // Possibile argomento di riferimento Null.
                CartCollection = JsonConvert.DeserializeObject<CartItemCollection>(jsonStringFromSession);
#pragma warning restore CS8604 // Possibile argomento di riferimento Null.
#pragma warning restore CS8601 // Possibile assegnazione di riferimento Null.
                totale = float.Parse(HttpContext?.Session.GetString("Totale"));
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Guid OrdineId = Guid.NewGuid();
                var CollectionOrder = _db.Ordine.Where(x => x.DataPrenotazione.Date == DateTime.Now.Date).ToList();
                connection.Open();
                int count = CollectionOrder.Any() ? CollectionOrder.Count + 1 : 1;
                foreach (var item in CartCollection)
                {

                    SqlCommand command = new SqlCommand("sp_InsertOrdine", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Aggiungi i parametri alla stored procedure
                    command.Parameters.AddWithValue("@OrdineID", OrdineId);
                    command.Parameters.AddWithValue("@Cliente", Cliente);
                    command.Parameters.AddWithValue("@Annullato", false);
                    command.Parameters.AddWithValue("@Consegnato", false);
                    command.Parameters.AddWithValue("@Bevanda", item.Bevanda); // Esempio
                    command.Parameters.AddWithValue("@InLavorazione", true);
                    command.Parameters.AddWithValue("@DataAnnullamento", DateTime.Now);
                    command.Parameters.AddWithValue("@DataPrenotazione", DateTime.Now);
                    command.Parameters.AddWithValue("@Note", item.Note); // Esempio
                    command.Parameters.AddWithValue("@Plus", item.Plus); // Esempio
                    command.Parameters.AddWithValue("@PaninoID", item.PaninoID); // Esempio
                    command.Parameters.AddWithValue("@Patatine", item.Patatine);
                    command.Parameters.AddWithValue("@PrezzoFinale", item.Prezzo);
                    command.Parameters.AddWithValue("@NumeroOrdine", count);
                    command.ExecuteNonQuery();
                    count++;
                }
                connection.Close();
            }
            CartCollection.Clear();
            CartCollection = new CartItemCollection();
            HttpContext.Session.SetString("Collection", "");
            HttpContext.Session.SetString("Totale", "0");
            HttpContext.Session.SetString("Count", "0");
            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult EditOrder(Guid OrdineID, int Ordine, string Scelta)
        {
            if (!String.IsNullOrEmpty(Scelta))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_ModificaOrdine", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrdineID", OrdineID);
                    command.Parameters.AddWithValue("@Annullato", Scelta == "Annulla" ? true : false);
                    command.Parameters.AddWithValue("@Consegnato", Scelta == "Consegnato" ? true : false);
                    command.Parameters.AddWithValue("@NumeroOrdine", Ordine);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Ordinazioni");
        }
        
        [HttpPost]
        public IActionResult AddOrEditPanino(Panino p)
        {
            var InMenu = false;
            if (!String.IsNullOrEmpty(p.InMenu))
            {
                if (p.InMenu == "on")
                {
                    InMenu = true;
                }
            }

			var PaninoMese = false;
			if (!String.IsNullOrEmpty(p.PaninoMese))
			{
				if (p.PaninoMese == "on")
				{
					PaninoMese = true;
				}
			}

			if (p.Immagine != null && p.Immagine.Length > 0)
            {
                if (p.Immagine != null && p.Immagine.Length > 0)
                {
                    try
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            // Posiziona il MemoryStream all'inizio dei dati
                            memoryStream.Position = 0;

                            // Copia i dati dell'immagine nel MemoryStream
                            p.Immagine.CopyTo(memoryStream);

                            // Crea un'istanza di Image dal MemoryStream
                            using (var image = Image.FromStream(memoryStream))
                            {
                                // Imposta il percorso di destinazione (ad esempio wwwroot)
                                var imagePath = Path.Combine("wwwroot", "Images", "Panini", p.PathImage);
                                p.PathImage = imagePath;
                                // Salva l'immagine sul disco
                                image.Save(imagePath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_ResyncPiatto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PaninoID", p.PaninoID);
                command.Parameters.AddWithValue("@Nome", p.Nome);
                command.Parameters.AddWithValue("@Prezzo", p.Prezzo);
                command.Parameters.AddWithValue("@PaninoMese", PaninoMese);
                command.Parameters.AddWithValue("@PathImage", p.PathImage);
                command.Parameters.AddWithValue("@InMenu", InMenu);
                command.Parameters.AddWithValue("@Descrizione", p.Descrizione);
                command.Parameters.AddWithValue("@Categoria", p.Categoria);
                command.Parameters.AddWithValue("@Add", p.New);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("EditMenu");
        }


        #region Comments

        /*
         public IActionResult AddOrEditPanino([FromBody] PaninoWithImageDto paninoDto)
        {
            // Esegui le operazioni necessarie per salvare l'immagine e i dati del panino nel database o in un file system
            try
            {
                // Esempio di salvataggio dell'immagine nel database
                // Assicurati di gestire correttamente le transazioni e gli errori
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_ResyncPiatto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PaninoID", paninoDto.Panino.PaninoID);
                    command.Parameters.AddWithValue("@Nome", paninoDto.Panino.Nome);
                    command.Parameters.AddWithValue("@Prezzo", paninoDto.Panino.Prezzo);
                    command.Parameters.AddWithValue("@PaninoMese", paninoDto.Panino.PaninoMese);
                    // Concatena il nome dell'immagine al percorso dell'immagine
                    string imagePath = $"wwwroot/Images/Panini/{paninoDto.Panino.Nome}_{DateTime.Now.Ticks}.jpg"; // Esempio di nome univoco basato sul timestamp
                    command.Parameters.AddWithValue("@PathImage", imagePath);
                    command.Parameters.AddWithValue("@InMenu", paninoDto.Panino.InMenu);
                    command.Parameters.AddWithValue("@Categoria", paninoDto.Panino.Categoria);
                    command.Parameters.AddWithValue("@Add", paninoDto.Panino.New);
                    command.Parameters.AddWithValue("@Image", paninoDto.Image); // Passa l'immagine come byte array
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return Ok(); // Ritorna un'indicazione di successo
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori
                return StatusCode(500, $"Errore durante il salvataggio del panino: {ex.Message}");
            }
        }
         
         
         */

        #endregion


        #endregion

        #region default methods
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion


        #endregion
    }
}







