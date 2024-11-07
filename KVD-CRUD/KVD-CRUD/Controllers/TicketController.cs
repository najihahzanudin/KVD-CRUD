using KVD_CRUD.Authenticate;
using KVD_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KVD_CRUD.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ApplicationDbContext context, ILogger<TicketController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tickets = _context.Tickets.Where(x => x.flagDelete == false).ToList();

            return View(tickets);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string username, string country, string phoneNumber)
        {
            var ticket = new Ticket
            {
                name = username,
                country = country,
                phoneNumber = phoneNumber,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            AuditLog("Create New Ticket", username);
            _logger.LogInformation("Create a new ticket for user {name} at {@createdAt}", ticket.name, ticket.createdAt);

            TempData["AlertMessage"] = "Created New Ticket Successfully!";

            return View(ticket);
        }

        public IActionResult Update(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(x => x.id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        public IActionResult Update(Ticket ticket, [FromForm(Name = "username")] string username)
        {
            ticket.name = username;

            if (ModelState.IsValid)
            {
                _context.Tickets.Update(ticket);
                _context.SaveChanges();

                AuditLog("Update Ticket", ticket.name);
                _logger.LogInformation("Update ticket for user {name} at {@createdAt}", ticket.name, ticket.createdAt);

                TempData["AlertMessage"] = "Update Ticket Successfully!";

                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Tickets.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            item.flagDelete = true;
            
            await _context.SaveChangesAsync();

            AuditLog("Delete Ticket", item.name);
            _logger.LogInformation("Delete ticket for user {name} at {@createdAt}", item.name, item.createdAt);

            TempData["AlertMessage"] = "Delete Ticket Successfully!";

            return RedirectToAction(nameof(Index));
        }


        public void AuditLog(string action, string username)
        {
            AuditLog alog = new AuditLog();
            alog.description = action;
            alog.createdAt = DateTime.Now;
            alog.username = username;
            _context.AuditLogs.Add(alog);
            _context.SaveChanges();
        }
    }
}
