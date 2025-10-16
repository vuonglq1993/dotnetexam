using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicContext _context;

        public RentalsController(ComicContext context)
        {
            _context = context;
        }

        // Trang thuê sách
        public async Task<IActionResult> Create()
        {
            ViewBag.Comics = await _context.ComicBooks.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DateTime rentalDate, DateTime returnDate, List<int> comicIds, List<int> quantities)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
                return RedirectToAction("Login", "Account");

            if (comicIds == null || comicIds.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one book");
                ViewBag.Comics = await _context.ComicBooks.ToListAsync();
                return View();
            }

            var rental = new Rental
            {
                CustomerID = customerId.Value,
                RentalDate = rentalDate,
                ReturnDate = returnDate,
                Status = "Đang thuê"
            };
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            // Thêm chi tiết
            for (int i = 0; i < comicIds.Count; i++)
            {
                var book = await _context.ComicBooks.FindAsync(comicIds[i]);
                if (book == null) continue;

                var detail = new RentalDetail
                {
                    RentalID = rental.RentalID,
                    ComicBookID = comicIds[i],
                    Quantity = quantities[i],
                    PricePerDay = book.PricePerDay
                };
                _context.RentalDetails.Add(detail);
            }
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thuê sách thành công!";
            return RedirectToAction("Report");
        }

        // 📊 Báo cáo thuê sách
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            var query = from rd in _context.RentalDetails
                        join r in _context.Rentals on rd.RentalID equals r.RentalID
                        join c in _context.Customers on r.CustomerID equals c.CustomerID
                        join b in _context.ComicBooks on rd.ComicBookID equals b.ComicBookID
                        select new
                        {
                            b.Title,
                            r.RentalDate,
                            r.ReturnDate,
                            c.FullName,
                            rd.Quantity
                        };

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(x => x.RentalDate >= startDate && x.ReturnDate <= endDate);
            }

            var result = await query
                .OrderBy(x => x.RentalDate)
                .ToListAsync();

            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(result);
        }
    }
}
