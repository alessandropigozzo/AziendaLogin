using LoginStartMenu.Models;
using LoginStartMenu.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
public class ImageBoxViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public ImageBoxViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    // Modificato per ricevere l'Id dell'utente
 

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            // Recupera l'utente con l'immagine associata
            var user = await _context.Utenti
                .Include(u => u.Immagine) // Includo l'immagine associata
                .FirstOrDefaultAsync(u => u.IdUtente == userId);

            if (user == null || user.Immagine == null)
                return Content("Nessun utente o immagine trovata");
        // Se l'immagine è in formato Base64, la converto in byte[]

        // Se vuoi passare direttamente i dati come stringa Base64
     //   user.Immagine.Img1 = Convert.ToBase64String(user.Immagine.Img1);

        return View(user.Immagine); // Passa l'immagine alla vista
        }
    

}