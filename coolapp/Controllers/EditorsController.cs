using coolapp.Models;
using coolapp.Models.Data;
using coolapp.ViewModels.Editors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coolapp.Controllers
{
    public class EditorsController : Controller
    {
        private readonly AppCtx _context;

        public EditorsController(AppCtx context)
        {
            _context = context;
        }

        // GET: Editors
        public async Task<IActionResult> Index()
        {
            // через контекст данных получаем доступ к таблице базы данных FormsOfStudy
            var appCtx = _context.Editors
                .OrderBy(f => f.NameOfEditor);          // сортируем все записи по имени форм обучения

            // возвращаем в представление полученный список записей
            return View(await appCtx.ToListAsync());
        }

        // GET: Editors/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Editors == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // GET: Editors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditorViewModel model)
        {
            if (_context.Editors
                .Where(f => f.NameOfEditor == model.NameOfEditor)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный исполнитель уже существует");
            }

            if (ModelState.IsValid)
            {
                Editor editor = new()
                {
                    NameOfEditor = model.NameOfEditor
                };

                _context.Add(editor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Editors/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Editors == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors.FindAsync(id);
            if (editor == null)
            {
                return NotFound();
            }

            EditEditorViewModel model = new()
            {
                Id = editor.Id,
                NameOfEditor = editor.NameOfEditor
            };
            return View(model);
        }

        // POST: Editors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditEditorViewModel model)
        {
            Editor editor = await _context.Editors.FindAsync(id);

            if (id != editor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    editor.NameOfEditor = model.NameOfEditor;
                    _context.Update(editor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorExists(editor.Id))
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

        // GET: Editors/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Editors == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // POST: Editors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Editors == null)
            {
                return Problem("Entity set 'AppCtx.Editors'  is null.");
            }
            var editor = await _context.Editors.FindAsync(id);
            if (editor != null)
            {
                _context.Editors.Remove(editor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorExists(short id)
        {
            return (_context.Editors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
