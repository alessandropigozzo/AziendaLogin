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


    public async Task<IViewComponentResult> InvokeAsync()
    {
        var users = await _context.Utenti
            .Include(u => u.Immagine)
            .ToListAsync();

        return View(users);
    }

}