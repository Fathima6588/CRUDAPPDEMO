using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDAPPDEMO.DATA;
using CRUDAPPDEMO.Models;

namespace CRUDAPPDEMO.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teachers
                .Include(t => t.Student)
                .ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var teacher = await _context.Teachers
                .Include(t => t.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher == null) return NotFound();

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Course,StudentId")] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Students = new SelectList(_context.Students, "Id", "Name", teacher.StudentId);
                return View(teacher);
            }

            _context.Add(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            ViewBag.Students = new SelectList(_context.Students, "Id", "Name", teacher.StudentId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Course,StudentId")] Teacher teacher)
        {
            if (id != teacher.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Students = new SelectList(_context.Students, "Id", "Name", teacher.StudentId);
                return View(teacher);
            }

            try
            {
                _context.Update(teacher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teachers.Any(e => e.Id == teacher.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var teacher = await _context.Teachers
                .Include(t => t.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher == null) return NotFound();

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
                _context.Teachers.Remove(teacher);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
