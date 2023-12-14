using coolapp.Models;
using coolapp.Models.Data;
using coolapp.ViewModels.EditorsForAlbums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace coolapp.Controllers
{
    public class EditorsForAlbumsController : Controller
    {
        private readonly AppCtx _context;

        public EditorsForAlbumsController(AppCtx context)
        {
            _context = context;
        }

        // GET: EditorsForAlbums
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["EditorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "editor_desc" : "";
            ViewData["AlbumSortParm"] = sortOrder == "Album" ? "album_desc" : "Album";
            ViewData["CurrentFilter"] = searchString;
            var editorsForAlbums = from s in _context.EditorsForAlbums
                         select s;
            switch (sortOrder)
            {
                case "editor_desc":
                    editorsForAlbums = editorsForAlbums.OrderByDescending(s => s.IdEditor);
                    break;
                case "Album":
                    editorsForAlbums = editorsForAlbums.OrderBy(s => s.IdAlbum);
                    break;
                case "album_desc":
                    editorsForAlbums = editorsForAlbums.OrderByDescending(s => s.IdAlbum);
                    break;
                default:
                    editorsForAlbums = editorsForAlbums.OrderBy(s => s.IdEditor);
                    break;
            }
            return View(await editorsForAlbums.Include(f => f.Editor).Include(f => f.Album).AsNoTracking().ToListAsync());
        }

        // GET: EditorsForAlbums/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.EditorsForAlbums == null)
            {
                return NotFound();
            }

            var editorsForAlbums = await _context.EditorsForAlbums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editorsForAlbums == null)
            {
                return NotFound();
            }

            return View(editorsForAlbums);
        }

        // GET:  EditorsForAlbums/Create
        public IActionResult Create()
        {
            ViewData["IdEditor"] = new SelectList(_context.Editors.OrderBy(o => o.NameOfEditor), "Id", "NameOfEditor");
            ViewData["IdAlbum"] = new SelectList(_context.Albums.OrderBy(o => o.NameAlbum), "Id", "NameAlbum");
            return View();
        }

        // POST: EditorsForAlbums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditorsForAlbumsViewModel model)
        {
            if (ModelState.IsValid)
            {
                EditorsForAlbums editorsForAlbums = new()
                {
                    IdEditor = model.IdEditor,
                    IdAlbum = model.IdAlbum,
                };

                _context.Add(editorsForAlbums);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdEditor"] = new SelectList(_context.Editors.OrderBy(o => o.NameOfEditor), "Id", "NameOfEditor", model.IdEditor);
            ViewData["IdAlbum"] = new SelectList(_context.Albums.OrderBy(o => o.NameAlbum), "Id", "NameAlbum", model.IdAlbum);
            return View(model);
        }

        // GET: EditorsForAlbums/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.EditorsForAlbums == null)
            {
                return NotFound();
            }

            var editorsForAlbums = await _context.EditorsForAlbums.FindAsync(id);
            if (editorsForAlbums == null)
            {
                return NotFound();
            }
            ViewData["IdEditor"] = new SelectList(_context.Editors, "Id", "NameOfEditor", editorsForAlbums.IdEditor);
            ViewData["IdAlbum"] = new SelectList(_context.Albums, "Id", "NameAlbum", editorsForAlbums.IdAlbum);
            return View(editorsForAlbums);
        }

        // POST: EditorsForAlbums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,IdEditor, IdAlbum")] EditorsForAlbums editorsForAlbums)
        {
            if (id != editorsForAlbums.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(editorsForAlbums);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorsForAlbumsExists(editorsForAlbums.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            ViewData["IdEditor"] = new SelectList(_context.Editors, "Id", "NameOfEditor", editorsForAlbums.IdEditor);
            ViewData["IdAlbum"] = new SelectList(_context.Albums, "Id", "NameAlbum", editorsForAlbums.IdAlbum);
            return View(editorsForAlbums);
        }

        // GET: EditorsForAlbums/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.EditorsForAlbums == null)
            {
                return NotFound();
            }

            var editorsForAlbums = await _context.EditorsForAlbums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editorsForAlbums == null)
            {
                return NotFound();
            }

            return View(editorsForAlbums);
        }

        // POST: EditorsForAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.EditorsForAlbums == null)
            {
                return Problem("Entity set 'AppCtx.EditorsForAlbums'  is null.");
            }
            var editorsForAlbums = await _context.EditorsForAlbums.FindAsync(id);
            if (editorsForAlbums != null)
            {
                _context.EditorsForAlbums.Remove(editorsForAlbums);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorsForAlbumsExists(int id)
        {
            return (_context.EditorsForAlbums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}