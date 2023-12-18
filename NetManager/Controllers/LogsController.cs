using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetManager.Data;
using NetManager.Models;

namespace NetManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : Controller
    {
        private readonly NetManagerContext _context;

        public LogsController(NetManagerContext context)
        {
            _context = context;
        }

        // GET: Logs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Logs != null ? 
                          Ok(await _context.Logs.ToListAsync()) :
                          Problem("Entity set 'NetManagerContext.Logs'  is null.");
        }

        // GET: Logs/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Logs == null)
            {
                return NotFound();
            }

            var logs = await _context.Logs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logs == null)
            {
                return NotFound();
            }

            return Ok(logs);
        }


        // POST: Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Id,Action,SqlRequest,CreatedDate")] Logs logs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(logs);
        }


        // POST: Logs/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Logs == null)
            {
                return Problem("Entity set 'NetManagerContext.Logs'  is null.");
            }
            var logs = await _context.Logs.FindAsync(id);
            if (logs != null)
            {
                _context.Logs.Remove(logs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogsExists(int id)
        {
          return (_context.Logs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
