using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedSeaArzu.Data;
using RedSeaArzu.Models;
using System.Diagnostics;

namespace RedSeaArzu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                Trips = await _context.Trips.ToListAsync(),
                Testimonials = await _context.Testimonials.Where(t => t.IsApproved).ToListAsync()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> TripDetails(int id)
        {
            var selectedTrip = await _context.Trips.FindAsync(id);

            if (selectedTrip == null)
            {
                return NotFound();
            }

            var otherTrips = await _context.Trips
                                           .Where(t => t.Id != id)
                                           .OrderBy(r => Guid.NewGuid()) 
                                           .Take(3)
                                           .ToListAsync();

            var viewModel = new TripDetailsViewModel
            {
                SelectedTrip = selectedTrip,
                OtherTrips = otherTrips
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                testimonial.IsApproved = false;
                _context.Testimonials.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
