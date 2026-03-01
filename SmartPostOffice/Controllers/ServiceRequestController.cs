using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPostOffice.Models;

namespace SmartPostOffice.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly PostDbContext _context;

        public ServiceRequestController(PostDbContext context)
        {
            _context = context;
        }

        // 1. Dashboard - සේවා ඉල්ලීම් සියල්ල පෙන්වීම
        public async Task<IActionResult> Index()
        {
            var requests = await _context.ServiceRequests
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
            return View(requests);
        }

        // 2. New Request Form - ෆෝම් එක පෙන්වීම
        public IActionResult Create()
        {
            return View();
        }

        // 3. Save Request - දත්ත ඩේටාබේස් එකට දැමීම
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest serviceRequest)
        {
            // Auto-generated fields සෙට් කිරීම
            serviceRequest.ReferenceNumber = "SP-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(1000, 9999);
            serviceRequest.CreatedDate = DateTime.Now;
            serviceRequest.Status = "PENDING";

            try 
            {
                // Validation එක නොබලා කෙලින්ම සේව් කරනවා (Bypass Validation)
                _context.Add(serviceRequest);
                await _context.SaveChangesAsync();
                
                // සාර්ථක නම් Index (Table) එකට යනවා
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                // ලොකු Error එකක් ආවොත් ටර්මිනල් එකේ මෙතනින් පෙන්වනවා
                Console.WriteLine("Database Error: " + ex.Message);
                return View(serviceRequest);
            }
        }

        // 4. Success Page (Optional)
        public IActionResult Success()
        {
            return View();
        }
    }
}