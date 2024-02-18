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
    public class TblGosteriSalonusController : Controller
    {
        private readonly TheaterClubContext _context;
        

        public TblGosteriSalonusController(TheaterClubContext context)
        {
            _context = context;
            
        }

        // GET: TblGosteriSalonus
        public async Task<IActionResult> Index()
        {
              return _context.TblGosteriSalonus != null ? 
                          View(await _context.TblGosteriSalonus.ToListAsync()) :
                          Problem("Entity set 'TheaterClubContext.TblGosteriSalonus'  is null.");
        }

        // GET: TblGosteriSalonus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblGosteriSalonus == null)
            {
                return NotFound();
            }

            var tblGosteriSalonu = await _context.TblGosteriSalonus
                .FirstOrDefaultAsync(m => m.SalonId == id);
            if (tblGosteriSalonu == null)
            {
                return NotFound();
            }

            return View(tblGosteriSalonu);
        }

        // GET: TblGosteriSalonus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblGosteriSalonus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalonId,SalonAd,SalonAdres,SalonTelNo,SalonEmail")] TblGosteriSalonu tblGosteriSalonu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblGosteriSalonu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblGosteriSalonu);
        }

        // GET: TblGosteriSalonus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblGosteriSalonus == null)
            {
                return NotFound();
            }

            var tblGosteriSalonu = await _context.TblGosteriSalonus.FindAsync(id);
            if (tblGosteriSalonu == null)
            {
                return NotFound();
            }
            return View(tblGosteriSalonu);
        }

        // POST: TblGosteriSalonus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalonId,SalonAd,SalonAdres,SalonTelNo,SalonEmail")] TblGosteriSalonu tblGosteriSalonu)
        {
            if (id != tblGosteriSalonu.SalonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblGosteriSalonu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblGosteriSalonuExists(tblGosteriSalonu.SalonId))
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
            return View(tblGosteriSalonu);
        }

        // GET: TblGosteriSalonus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblGosteriSalonus == null)
            {
                return NotFound();
            }

            var tblGosteriSalonu = await _context.TblGosteriSalonus
                .FirstOrDefaultAsync(m => m.SalonId == id);
            if (tblGosteriSalonu == null)
            {
                return NotFound();
            }

            return View(tblGosteriSalonu);
        }

        // POST: TblGosteriSalonus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblGosteriSalonus == null)
            {
                return Problem("Entity set 'TheaterClubContext.TblGosteriSalonus'  is null.");
            }
            var tblGosteriSalonu = await _context.TblGosteriSalonus.FindAsync(id);
            if (tblGosteriSalonu != null)
            {
                _context.TblGosteriSalonus.Remove(tblGosteriSalonu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblGosteriSalonuExists(int id)
        {
          return (_context.TblGosteriSalonus?.Any(e => e.SalonId == id)).GetValueOrDefault();
        }
    }
}
