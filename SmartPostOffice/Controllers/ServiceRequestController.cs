using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPostOffice.Models;

namespace SmartPostOffice.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly PostDbContext _context;

        // Database context eka controller ekata connect karagannawa
        public ServiceRequestController(PostDbContext context)
        {
            _context = context;
        }

        // 1. New Service Request Form eka pennana action eka
        public IActionResult Create()
        {
            return View();
        }

        // 2. Form eka submit karama data save karana action eka
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(ServiceRequest serviceRequest)
        {
            ModelState.Remove("ReferenceNumber"); // ReferenceNumber එක automatic set වෙන නිසා validation එකෙන් ඉවත් කරනවා
            // 1. Automatic data ටික මෙතනින් සෙට් වෙනවා
            serviceRequest.ReferenceNumber = "SP-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(1000, 9999);
            serviceRequest.CreatedDate = DateTime.Now;
            serviceRequest.Status = "PENDING";
            
            // Database එකේ Error එකක් එන එක නවත්තන්න මේක දාන්න (SenderID null වෙන්න බැරි නිසා)
            // serviceRequest.SenderId = 1; 

            if (ModelState.IsValid)
            {
                _context.Add(serviceRequest);
                await _context.SaveChangesAsync();
                
                // සාර්ථකව සේව් වුණාම Table එකට යමු
                return RedirectToAction(nameof(Index));
            }
            
            // වැරැද්දක් තිබ්බොත් ආයේ Form එකම පෙන්නනවා (Validation errors එක්ක)
            return View(serviceRequest);
        }

        public IActionResult Success()
        {
            return View();
        }
        // මේක කලින් හදපු controller එක ඇතුළටම දාන්න
        public async Task<IActionResult> Index()
        {
            // Database එකේ තියෙන සේරම requests ටික අරන් list එකක් විදිහට එවනවා
            var requests = await _context.ServiceRequests.OrderByDescending(r => r.CreatedDate).ToListAsync();
            return View(requests);
        }
    }
}