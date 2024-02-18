using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterClubProject.Models;

namespace TheaterClubProject.Controllers
{
    public class TblSaAlansController : Controller
    {
        private readonly TheaterClubContext _context;

        public TblSaAlansController(TheaterClubContext context)
        {
            _context = context;
        }

        // GET: TblSaAlans
        public async Task<IActionResult> Index()
        {
              return _context.TblSaAlans != null ? 
                          View(await _context.TblSaAlans.ToListAsync()) :
                          Problem("Entity set 'TheaterClubContext.TblSaAlans'  is null.");
        }

        // GET: TblSaAlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblSaAlans == null)
            {
                return NotFound();
            }

            var tblSaAlan = await _context.TblSaAlans
                .FirstOrDefaultAsync(m => m.SaAlanId == id);
            if (tblSaAlan == null)
            {
                return NotFound();
            }

            return View(tblSaAlan);
        }

        // GET: TblSaAlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblSaAlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaAlanId,SaAlan")] TblSaAlan tblSaAlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblSaAlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblSaAlan);
        }

        // GET: TblSaAlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblSaAlans == null)
            {
                return NotFound();
            }

            var tblSaAlan = await _context.TblSaAlans.FindAsync(id);
            if (tblSaAlan == null)
            {
                return NotFound();
            }
            return View(tblSaAlan);
        }

        // POST: TblSaAlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaAlanId,SaAlan")] TblSaAlan tblSaAlan)
        {
            if (id != tblSaAlan.SaAlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSaAlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSaAlanExists(tblSaAlan.SaAlanId))
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
            return View(tblSaAlan);
        }

        // GET: TblSaAlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblSaAlans == null)
            {
                return NotFound();
            }

            var tblSaAlan = await _context.TblSaAlans
                .FirstOrDefaultAsync(m => m.SaAlanId == id);
            if (tblSaAlan == null)
            {
                return NotFound();
            }

            return View(tblSaAlan);
        }

        // POST: TblSaAlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblSaAlans == null)
            {
                return Problem("Entity set 'TheaterClubContext.TblSaAlans'  is null.");
            }
            var tblSaAlan = await _context.TblSaAlans.FindAsync(id);
            if (tblSaAlan != null)
            {
                _context.TblSaAlans.Remove(tblSaAlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSaAlanExists(int id)
        {
          return (_context.TblSaAlans?.Any(e => e.SaAlanId == id)).GetValueOrDefault();
        }
    }
}
