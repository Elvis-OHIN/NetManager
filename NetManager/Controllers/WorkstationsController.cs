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
    public class WorkstationsController : Controller
    {
        private readonly NetManagerContext _context;

        public WorkstationsController(NetManagerContext context)
        {
            _context = context;
        }

        // GET: Workstations
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Workstation != null ? 
                          Ok(await _context.Workstation.ToListAsync()) :
                          Problem("Entity set 'NetManagerContext.Workstation'  is null.");
        }

        // GET: Workstations/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workstation == null)
            {
                return NotFound();
            }

            var workstation = await _context.Workstation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workstation == null)
            {
                return NotFound();
            }

            return Ok(workstation);
        }


        // POST: Workstations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,Numero,Statut,IsActive,IsEnable,CreatedDate,UpdatedDate")] Workstation workstation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workstation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(workstation);
        }


        // POST: Workstations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Numero,Statut,IsActive,IsEnable,CreatedDate,UpdatedDate")] Workstation workstation)
        {
            if (id != workstation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workstation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkstationExists(workstation.Id))
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
            return Ok(workstation);
        }


        // POST: Workstations/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workstation == null)
            {
                return Problem("Entity set 'NetManagerContext.Workstation'  is null.");
            }
            var workstation = await _context.Workstation.FindAsync(id);
            if (workstation != null)
            {
                _context.Workstation.Remove(workstation);
            }
            
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool WorkstationExists(int id)
        {
          return (_context.Workstation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
