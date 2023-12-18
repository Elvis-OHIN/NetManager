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
    public class ParcsController : Controller
    {
        private readonly NetManagerContext _context;

        public ParcsController(NetManagerContext context)
        {
            _context = context;
        }

        // GET: Parcs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Parcs != null ? 
                          Ok(await _context.Parcs.ToListAsync()) :
                          Problem("Entity set 'NetManagerContext.Parcs'  is null.");
        }

        // GET: Parcs/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parcs == null)
            {
                return NotFound();
            }

            var parcs = await _context.Parcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcs == null)
            {
                return NotFound();
            }

            return Ok(parcs);
        }


        // POST: Parcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive,IsEnable,CreatedDate,UpdatedDate")] Parcs parcs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(parcs);
        }

       
        // POST: Parcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive,IsEnable,CreatedDate,UpdatedDate")] Parcs parcs)
        {
            if (id != parcs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcsExists(parcs.Id))
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
            return Ok(parcs);
        }


        // POST: Parcs/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parcs == null)
            {
                return Problem("Entity set 'NetManagerContext.Parcs'  is null.");
            }
            var parcs = await _context.Parcs.FindAsync(id);
            if (parcs != null)
            {
                _context.Parcs.Remove(parcs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcsExists(int id)
        {
          return (_context.Parcs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
