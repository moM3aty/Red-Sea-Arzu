using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedSeaArzu.Data;
using RedSeaArzu.Models;

namespace RedSeaArzu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TotalTrips = await _context.Trips.CountAsync(),
                PendingTestimonials = await _context.Testimonials.CountAsync(t => !t.IsApproved)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Trips()
        {
            return View(await _context.Trips.ToListAsync());
        }

        public IActionResult CreateTrip()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrip(Trip trip, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    trip.ImageUrl = await UploadImage(imageFile);
                }
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Trips));
            }
            return View(trip);
        }

        public async Task<IActionResult> EditTrip(int? id)
        {
            if (id == null) return NotFound();
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null) return NotFound();
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrip(int id, Trip trip, IFormFile? imageFile)
        {
            if (id != trip.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var tripFromDb = await _context.Trips.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                string currentImageUrl = tripFromDb?.ImageUrl ?? "";

                if (imageFile != null)
                {
                    if (!string.IsNullOrEmpty(currentImageUrl))
                    {
                        DeleteImage(currentImageUrl);
                    }
                    trip.ImageUrl = await UploadImage(imageFile);
                }
                else
                {
                    trip.ImageUrl = currentImageUrl;
                }
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trips.Any(e => e.Id == trip.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Trips));
            }
            return View(trip);
        }

        public async Task<IActionResult> DeleteTrip(int? id)
        {
            if (id == null) return NotFound();
            var trip = await _context.Trips.FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null) return NotFound();
            return View(trip);
        }

        [HttpPost, ActionName("DeleteTrip")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTripConfirmed(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip != null)
            {
                if (!string.IsNullOrEmpty(trip.ImageUrl))
                {
                    DeleteImage(trip.ImageUrl);
                }
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Trips));
        }

        public async Task<IActionResult> PendingTestimonials()
        {
            var pending = await _context.Testimonials.Where(t => !t.IsApproved).ToListAsync();
            return View(pending);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                testimonial.IsApproved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(PendingTestimonials));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(PendingTestimonials));
        }

        private async Task<string> UploadImage(IFormFile imageFile)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "trips");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/trips/" + uniqueFileName;
        }

        private void DeleteImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;
            string imagePath = Path.Combine(_hostEnvironment.WebRootPath, imageUrl.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}