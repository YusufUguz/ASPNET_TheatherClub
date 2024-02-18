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
    public class TblOyuncusController : Controller
    {
        private readonly TheaterClubContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TblOyuncusController(TheaterClubContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: TblOyuncus
        public async Task<IActionResult> Index()
        {
              return _context.TblOyuncus != null ? 
                          View(await _context.TblOyuncus.ToListAsync()) :
                          Problem("Entity set 'TheaterClubContext.TblOyuncus'  is null.");
        }

        // GET: TblOyuncus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblOyuncus == null)
            {
                return NotFound();
            }

            var tblOyuncu = await _context.TblOyuncus
                .FirstOrDefaultAsync(m => m.OyuncuId == id);
            if (tblOyuncu == null)
            {
                return NotFound();
            }

            return View(tblOyuncu);
        }

        // GET: TblOyuncus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblOyuncus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OyuncuId,ImageFile,OyuncuAdSoyad,OyuncuTelNo,OyuncuEmail,OyuncuDogumTarihi,OyuncuDogumYeri,OyuncuMezunUni")] TblOyuncu tblOyuncu)
        {
            //if (ModelState.IsValid)
            //{
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblOyuncu.ImageFile.FileName);
                string extension = Path.GetExtension(tblOyuncu.ImageFile.FileName);
                tblOyuncu.OyuncuFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath + "/Images/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblOyuncu.ImageFile.CopyToAsync(filestream);
                }

                _context.Add(tblOyuncu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(tblOyuncu);
        }

        // GET: TblOyuncus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblOyuncus == null)
            {
                return NotFound();
            }

            var tblOyuncu = await _context.TblOyuncus.FindAsync(id);
            if (tblOyuncu == null)
            {
                return NotFound();
            }
            return View(tblOyuncu);
        }

        // POST: TblOyuncus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OyuncuId,ImageFile,OyuncuAdSoyad,OyuncuTelNo,OyuncuEmail,OyuncuDogumTarihi,OyuncuDogumYeri,OyuncuMezunUni")] TblOyuncu tblOyuncu)
        {
            if (id != tblOyuncu.OyuncuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblOyuncu.ImageFile.FileName);
                string extension = Path.GetExtension(tblOyuncu.ImageFile.FileName);
                tblOyuncu.OyuncuFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath + "/Images/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblOyuncu.ImageFile.CopyToAsync(filestream);
                }
                try
                {
                    _context.Update(tblOyuncu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOyuncuExists(tblOyuncu.OyuncuId))
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
            return View(tblOyuncu);
        }

        // GET: TblOyuncus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblOyuncus == null)
            {
                return NotFound();
            }

            var tblOyuncu = await _context.TblOyuncus
                .FirstOrDefaultAsync(m => m.OyuncuId == id);
            if (tblOyuncu == null)
            {
                return NotFound();
            }

            return View(tblOyuncu);
        }

        // POST: TblOyuncus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblOyuncus == null)
            {
                return Problem("Entity set 'TheaterClubContext.TblOyuncus'  is null.");
            }
            var tblOyuncu = await _context.TblOyuncus.FindAsync(id);
            if (tblOyuncu != null)
            {
                _context.TblOyuncus.Remove(tblOyuncu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOyuncuExists(int id)
        {
          return (_context.TblOyuncus?.Any(e => e.OyuncuId == id)).GetValueOrDefault();
        }
    }
}
