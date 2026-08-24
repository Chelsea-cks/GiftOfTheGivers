using GiftOfTheGivers.Data;
using GiftOfTheGivers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGivers.Controllers;

public class VolunteerController(ApplicationDbContext db, UserManager<ApplicationUser> users) : Controller
{
    private readonly ApplicationDbContext _db = db;
    private readonly UserManager<ApplicationUser> _users = users;

    [HttpGet]
    public IActionResult Create() => View(new Volunteer());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Volunteer volunteer)
    {
        if (!ModelState.IsValid)
            return View(volunteer);

        volunteer.UserID = User.Identity?.IsAuthenticated == true
            ? _users.GetUserId(User)
            : null;
        volunteer.Status = "Pending";
        volunteer.DateRegistered = DateTime.UtcNow;

        _db.Volunteers.Add(volunteer);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Success));
    }

    [HttpGet]
    public IActionResult Success() => View();
}
