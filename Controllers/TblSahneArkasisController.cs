using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterClubProject.Models;

namespace TheaterClubProject.Controllers
{
    [Authorize]
    public class TblSahneArkasisController : Controller
    {
        private readonly TheaterClubContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TblSahneArkasisController(TheaterClubContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: TblSahneArkasis
        public async Task<IActionResult> Index()
        {
            var theaterClubContext = _context.TblSahneArkasis.Include(t => t.SaAlan);
            return View(await theaterClubContext.ToListAsync());
        }

        // GET: TblSahneArkasis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblSahneArkasis == null)
            {
                return NotFound();
            }

            var tblSahneArkasi = await _context.TblSahneArkasis
                .Include(t => t.SaAlan)
                .FirstOrDefaultAsync(m => m.SahneArkasiId == id);
            if (tblSahneArkasi == null)
            {
                return NotFound();
            }

            return View(tblSahneArkasi);
        }

        // GET: TblSahneArkasis/Create
        public IActionResult Create()
        {
            ViewData["SaAlanId"] = new SelectList(_context.TblSaAlans, "SaAlanId", "SaAlan");
            return View();
        }

        // POST: TblSahneArkasis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SahneArkasiId,ImageFile,SaAdSoyad,SaTelNo,SaEmail,SaAlanId,SaDogumTarihi")] TblSahneArkasi tblSahneArkasi)
        {
            //if (ModelState.IsValid)
            //{
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblSahneArkasi.ImageFile.FileName);
                string extension = Path.GetExtension(tblSahneArkasi.ImageFile.FileName);
                tblSahneArkasi.SaFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath + "/Images/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblSahneArkasi.ImageFile.CopyToAsync(filestream);
                }
                _context.Add(tblSahneArkasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["SaAlanId"] = new SelectList(_context.TblSaAlans, "SaAlanId", "SaAlanId", tblSahneArkasi.SaAlanId);
            return View(tblSahneArkasi);
        }

        // GET: TblSahneArkasis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblSahneArkasis == null)
            {
                return NotFound();
            }

            var tblSahneArkasi = await _context.TblSahneArkasis.FindAsync(id);
            if (tblSahneArkasi == null)
            {
                return NotFound();
            }
            ViewData["SaAlanId"] = new SelectList(_context.TblSaAlans, "SaAlanId", "SaAlan", tblSahneArkasi.SaAlanId);
            return View(tblSahneArkasi);
        }

        // POST: TblSahneArkasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SahneArkasiId,ImageFile,SaAdSoyad,SaTelNo,SaEmail,SaAlanId,SaDogumTarihi")] TblSahneArkasi tblSahneArkasi)
        {
            if (id != tblSahneArkasi.SahneArkasiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblSahneArkasi.ImageFile.FileName);
                string extension = Path.GetExtension(tblSahneArkasi.ImageFile.FileName);
                tblSahneArkasi.SaFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath + "/Images/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblSahneArkasi.ImageFile.CopyToAsync(filestream);
                }

                try
                {
                    _context.Update(tblSahneArkasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSahneArkasiExists(tblSahneArkasi.SahneArkasiId))
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
            ViewData["SaAlanId"] = new SelectList(_context.TblSaAlans, "SaAlanId", "SaAlanId", tblSahneArkasi.SaAlanId);
            return View(tblSahneArkasi);
        }

        // GET: TblSahneArkasis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblSahneArkasis == null)
            {
                return NotFound();
            }

            var tblSahneArkasi = await _context.TblSahneArkasis
                .Include(t => t.SaAlan)
                .FirstOrDefaultAsync(m => m.SahneArkasiId == id);
            if (tblSahneArkasi == null)
            {
                return NotFound();
            }

            return View(tblSahneArkasi);
        }

        // POST: TblSahneArkasis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblSahneArkasis == null)
            {
                return Problem("Entity set 'TheaterClubContext.TblSahneArkasis'  is null.");
            }
            var tblSahneArkasi = await _context.TblSahneArkasis.FindAsync(id);
            if (tblSahneArkasi != null)
            {
                _context.TblSahneArkasis.Remove(tblSahneArkasi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSahneArkasiExists(int id)
        {
          return (_context.TblSahneArkasis?.Any(e => e.SahneArkasiId == id)).GetValueOrDefault();
        }
    }
}
