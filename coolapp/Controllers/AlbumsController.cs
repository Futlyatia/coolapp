using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coolapp.Models;
using coolapp.Models.Data;
using coolapp.ViewModels.Albums;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;

namespace coolapp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly AppCtx _context;

        public AlbumsController(AppCtx context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var albums = from s in _context.Albums
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    albums = albums.OrderByDescending(s => s.NameAlbum);
                    break;
                case "Date":
                    albums = albums.OrderBy(s => s.DatePost);
                    break;
                case "date_desc":
                    albums = albums.OrderByDescending(s => s.DatePost);
                    break;
                default:
                    albums = albums.OrderBy(s => s.NameAlbum);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.NameAlbum.Contains(searchString)).OrderBy(f => f.NameAlbum);
            }
            // через контекст данных получаем доступ к таблице базы данных FormsOfStudy

            // возвращаем в представление полученный список записей
            return View(await albums.AsNoTracking().ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAlbumViewModel model)
        {
            if (_context.Albums
                .Where(f => f.NameAlbum == model.NameAlbum)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный альбом уже существует");
            }

            if (ModelState.IsValid)
            {
                Album album = new()
                {
                    NameAlbum = model.NameAlbum,
                    Cover = model.Cover,
                    DatePost = DateTime.Now
                };

                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            EditAlbumViewModel model = new()
            {
                Id = album.Id,
                NameAlbum = album.NameAlbum,
                Cover = album.Cover,
            };
            return View(model);
        }

        // POST: Albums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditAlbumViewModel model)
        {
            Album album = await _context.Albums.FindAsync(id);

            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    album.NameAlbum = model.NameAlbum;
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            return View(model);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'AppCtx.Albums'  is null.");
            }
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(short id)
        {
            return (_context.Albums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
