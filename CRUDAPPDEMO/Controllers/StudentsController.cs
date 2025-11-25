using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDAPPDEMO.DATA;
using CRUDAPPDEMO.Models;
using System.Diagnostics;

namespace CRUDAPPDEMO.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());     
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Course,TeacherId")] Student student)

        {
            // debug incoming model to Output window
            Debug.WriteLine($"[DEBUG] Create POST model: Name='{student?.Name}', Email='{student?.Email}', Course='{student?.Course}', TeacherId='{student?.TeacherId}'");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    Debug.WriteLine("[DEBUG] SaveChangesAsync completed successfully.");
                    TempData["SuccessMessage"] = "Student added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    Debug.WriteLine("[ERROR] DbUpdateException: " + dbEx);
                    ModelState.AddModelError("", "Unable to save. Contact administrator.");
                    ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "Name", student?.TeacherId);
                    LogModelStateErrors();
                    return View(student);
                }
            }
            
            // If we got this far, something failed; redisplay form
            ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "Name", student?.TeacherId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "Name", student.TeacherId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Course,TeacherId")] Student student)
        {
            if (id != student.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                LogModelStateErrors();
                ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "Name", student.TeacherId);
                return View(student);
            }

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Student updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.Id)) return NotFound();
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                Debug.WriteLine(dbEx);
                ModelState.AddModelError("", "Unable to save changes. Try again later.");
                LogModelStateErrors();
                ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "Name", student.TeacherId);
                return View(student);
            }
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Student deleted.";
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        // Helper to print ModelState errors to output for debugging
        private void LogModelStateErrors()
        {
            foreach (var kvp in ModelState)
            {
                var key = kvp.Key;
                var errors = kvp.Value.Errors;
                foreach (var err in errors)
                {
                    Debug.WriteLine($"ModelState error for '{key}': {err.ErrorMessage} | Exception: {err.Exception}");
                }
            }
        }
    }
}