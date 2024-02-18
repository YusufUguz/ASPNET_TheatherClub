using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterClubProject.Models;

namespace TheaterClubProject.Controllers
{
    [Authorize]
    
    public class TblOyunsController : Controller
    {
        private readonly TheaterClubContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TblOyunsController(TheaterClubContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }

        // GET: TblOyuns
        public async Task<IActionResult> Index()
        {
              return _context.TblOyuns != null ? 
                          View(await _context.TblOyuns.ToListAsync()) :
                          Problem("Entity set 'TheaterClubContext.TblOyuns'  is null.");
        }

        

        // GET: TblOyuns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblOyuns == null)
            {
                return NotFound();
            }

            var tblOyun = await _context.TblOyuns
                .FirstOrDefaultAsync(m => m.OyunId == id);
            if (tblOyun == null)
            {
                return NotFound();
            }

            return View(tblOyun);
        }

        // GET: TblOyuns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblOyuns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OyunId,ImageFile,OyunAd,OyunTur,OyunDakika,OyunYasSiniri")] TblOyun tblOyun)
        {
            //if (ModelState.IsValid)
            //{

                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblOyun.ImageFile.FileName);
                string extension=Path.GetExtension(tblOyun.ImageFile.FileName);
                tblOyun.OyunFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath+"/Images/",fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblOyun.ImageFile.CopyToAsync(filestream);
                }

                _context.Add(tblOyun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(tblOyun);
        }

        // GET: TblOyuns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblOyuns == null)
            {
                return NotFound();
            }

            var tblOyun = await _context.TblOyuns.FindAsync(id);
            if (tblOyun == null)
            {
                return NotFound();
            }
            return View(tblOyun);
        }

        // POST: TblOyuns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OyunId,ImageFile,OyunAd,OyunTur,OyunDakika,OyunYasSiniri")] TblOyun tblOyun)
        {
            if (id != tblOyun.OyunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tblOyun.ImageFile.FileName);
                string extension = Path.GetExtension(tblOyun.ImageFile.FileName);
                tblOyun.OyunFoto = fileName = fileName + extension;
                string path = Path.Combine(wwwrootpath + "/Images/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await tblOyun.ImageFile.CopyToAsync(filestream);
                }
                try
                {
                    _context.Update(tblOyun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOyunExists(tblOyun.OyunId))
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
            return View(tblOyun);
        }

        // GET: TblOyuns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblOyuns == null)
            {
                return NotFound();
            }

            var tblOyun = await _context.TblOyuns
                .FirstOrDefaultAsync(m => m.OyunId == id);
            if (tblOyun == null)
            {
                return NotFound();
            }

            return View(tblOyun);
        }

        // POST: TblOyuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblOyuns == null)
            {
                return Problem("Entity set 'TheaterClubContext.TblOyuns'  is null.");
            }
            var tblOyun = await _context.TblOyuns.FindAsync(id);
            if (tblOyun != null)
            {
                _context.TblOyuns.Remove(tblOyun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOyunExists(int id)
        {
          return (_context.TblOyuns?.Any(e => e.OyunId == id)).GetValueOrDefault();
        }
    }
}
