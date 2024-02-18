using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TheaterClubProject.Models;

namespace TheaterClubProject.Controllers
{
    [Authorize]

    public class TblGosteriTakipsController : Controller
    {
        private readonly TheaterClubContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public TblGosteriTakipsController(TheaterClubContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: TblGosteriTakips
        public async Task<IActionResult> Index()
        {
            var theaterClubContext = _context.TblGosteriTakips.Include(t => t.GosteriOyun).Include(t => t.GosteriSalon).Include(t => t.GosteriSehir).Include(t => t.GosteriSehirilce);
            return View(await theaterClubContext.ToListAsync());
        }

        // GET: TblGosteriTakips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblGosteriTakips == null)
            {
                return NotFound();
            }

            var tblGosteriTakip = await _context.TblGosteriTakips
                .Include(t => t.GosteriOyun)
                .Include(t => t.GosteriSalon)
                .Include(t => t.GosteriSehir)
                .Include(t => t.GosteriSehirilce)
                .FirstOrDefaultAsync(m => m.GosteriId == id);
            if (tblGosteriTakip == null)
            {
                return NotFound();
            }

            return View(tblGosteriTakip);
        }


        // GET: TblGosteriTakips/Create


        public IActionResult Create()
        {

            List<Sehir> sehirlistesi = new List<Sehir>();
            sehirlistesi = (from Sehir in _context.Sehirs
                            select
                            Sehir).ToList();
            sehirlistesi.Insert(0, new Sehir { SehirId = 0, SehirBilgi = "Şehir Seçiniz" });
            ViewBag.ListofSehir = sehirlistesi;

            ViewData["GosteriOyunId"] = new SelectList(_context.TblOyuns, "OyunId", "OyunAd");
            ViewData["GosteriSalonId"] = new SelectList(_context.TblGosteriSalonus, "SalonId", "SalonAd");
            ViewData["GosteriSehirId"] = new SelectList(_context.Sehirs, "SehirId", "SehirBilgi");
            ViewData["GosteriSehirilceId"] = new SelectList(_context.Sehirilces, "SehirilceId", "SehirilceBilgi");

            return View();
        }

        public JsonResult GetSehirilce(int sehirId)
        {
            //List<Sehirilce> sehirilcelist = _context.Sehirilces.Where(x => x.SehirId == sehirId).ToList();
            var sehirilcelist = (from sehirilce in _context.Sehirilces
                                             where sehirilce.SehirId == sehirId
                                             select new
                                             {
                                                 Text = sehirilce.SehirilceBilgi,
                                                 Value = sehirilce.SehirilceId
                                             }).ToList();

            return Json(sehirilcelist, new System.Text.Json.JsonSerializerOptions());
        }


        // POST: TblGosteriTakips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GosteriId,ImageFile,GosteriAd,GosteriOyunId,GosteriOyuncular,GosteriSahneArkasi,GosteriSalonId,GosteriSehirId,GosteriSehirilceId,GosteriFiyat,GosteriTarih")] TblGosteriTakip tblGosteriTakip)
        {
            
            //if (ModelState.IsValid)
            //{
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblGosteriTakip.ImageFile.FileName);
                string extension = Path.GetExtension(tblGosteriTakip.ImageFile.FileName);
                tblGosteriTakip.GosteriFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath + "/Images/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblGosteriTakip.ImageFile.CopyToAsync(filestream);
                }

            _context.Add(tblGosteriTakip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}

            List<Sehir> sehirlistesi = new List<Sehir>();
            sehirlistesi = (from Sehir in _context.Sehirs
                            select
                            Sehir).ToList();
            sehirlistesi.Insert(0, new Sehir { SehirId = 0, SehirBilgi = "Şehir Seçiniz" });
            ViewBag.ListofSehir = sehirlistesi;

            ViewData["GosteriOyunId"] = new SelectList(_context.TblOyuns, "OyunId", "OyunId", tblGosteriTakip.GosteriOyunId);
            ViewData["GosteriSalonId"] = new SelectList(_context.TblGosteriSalonus, "SalonId", "SalonId", tblGosteriTakip.GosteriSalonId);
            ViewData["GosteriSehirId"] = new SelectList(_context.Sehirs, "SehirId", "SehirBilgi", tblGosteriTakip.GosteriSehirId);
            ViewData["GosteriSehirilceId"] = new SelectList(_context.Sehirilces, "SehirilceId", "SehirilceBilgi", tblGosteriTakip.GosteriSehirilceId);
              
            return View(tblGosteriTakip);
        }

        // GET: TblGosteriTakips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.TblGosteriTakips == null)
            {
                return NotFound();
            }

            var tblGosteriTakip = await _context.TblGosteriTakips.FindAsync(id);
            if (tblGosteriTakip == null)
            {
                return NotFound();
            }
            ViewData["GosteriOyunId"] = new SelectList(_context.TblOyuns, "OyunId", "OyunAd", tblGosteriTakip.GosteriOyunId);
            ViewData["GosteriSalonId"] = new SelectList(_context.TblGosteriSalonus, "SalonId", "SalonAd", tblGosteriTakip.GosteriSalonId);
            ViewData["GosteriSehirId"] = new SelectList(_context.Sehirs, "SehirId", "SehirId", tblGosteriTakip.GosteriSehirId);
            ViewData["GosteriSehirilceId"] = new SelectList(_context.Sehirilces, "SehirilceId", "SehirilceId", tblGosteriTakip.GosteriSehirilceId);
            return View(tblGosteriTakip);
        }

        // POST: TblGosteriTakips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GosteriId,ImageFile,GosteriAd,GosteriOyunId,GosteriOyuncular,GosteriSahneArkasi,GosteriSalonId,GosteriSehirId,GosteriSehirilceId,GosteriFiyat,GosteriTarih")] TblGosteriTakip tblGosteriTakip)
        {
            if (id != tblGosteriTakip.GosteriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblGosteriTakip.ImageFile.FileName);
                string extension = Path.GetExtension(tblGosteriTakip.ImageFile.FileName);
                tblGosteriTakip.GosteriFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath + "/Images/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblGosteriTakip.ImageFile.CopyToAsync(filestream);
                }

                try
                {
                    _context.Update(tblGosteriTakip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblGosteriTakipExists(tblGosteriTakip.GosteriId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GosteriOyunId"] = new SelectList(_context.TblOyuns, "OyunId", "OyunAd", tblGosteriTakip.GosteriOyunId);
            ViewData["GosteriSalonId"] = new SelectList(_context.TblGosteriSalonus, "SalonId", "SalonAd", tblGosteriTakip.GosteriSalonId);
            ViewData["GosteriSehirId"] = new SelectList(_context.Sehirs, "SehirId", "SehirId", tblGosteriTakip.GosteriSehirId);
            ViewData["GosteriSehirilceId"] = new SelectList(_context.Sehirilces, "SehirilceId", "SehirilceId", tblGosteriTakip.GosteriSehirilceId);
            return View(tblGosteriTakip);
        }

        // GET: TblGosteriTakips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblGosteriTakips == null)
            {
                return NotFound();
            }

            var tblGosteriTakip = await _context.TblGosteriTakips
                .Include(t => t.GosteriOyun)
                .Include(t => t.GosteriSalon)
                .Include(t => t.GosteriSehir)
                .Include(t => t.GosteriSehirilce)
                .FirstOrDefaultAsync(m => m.GosteriId == id);
            if (tblGosteriTakip == null)
            {
                return NotFound();
            }

            return View(tblGosteriTakip);
        }

        // POST: TblGosteriTakips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblGosteriTakips == null)
            {
                return Problem("Entity set 'TheaterClubContext.TblGosteriTakips'  is null.");
            }
            var tblGosteriTakip = await _context.TblGosteriTakips.FindAsync(id);
            if (tblGosteriTakip != null)
            {
                _context.TblGosteriTakips.Remove(tblGosteriTakip);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblGosteriTakipExists(int id)
        {
          return (_context.TblGosteriTakips?.Any(e => e.GosteriId == id)).GetValueOrDefault();
        }
    }
}
