using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vet.Data;
using Vet.Models;

namespace Vet.Controllers
{
    public class VeterinsController : Controller
    {
        private readonly VetsDB bd;

        public VeterinsController(VetsDB context)
        {
            bd = context;
        }

        // GET: Veterins
        public async Task<IActionResult> Index()
        {
            return View(await bd.Veterinarios.ToListAsync());
        }

        // GET: Veterins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterin = await bd.Veterinarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veterin == null)
            {
                return NotFound();
            }

            return View(veterin);
        }

        // GET: Veterins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,NumCedulaProf,Fotografia")] Veterin veterin)
        {
            if (ModelState.IsValid)
            {
                bd.Add(veterin);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterin);
        }

        // GET: Veterins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterin = await bd.Veterinarios.FindAsync(id);
            if (veterin == null)
            {
                return NotFound();
            }
            return View(veterin);
        }

        // POST: Veterins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,NumCedulaProf,Fotografia")] Veterin veterin)
        {
            if (id != veterin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(veterin);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinExists(veterin.ID))
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
            return View(veterin);
        }

        // GET: Veterins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterin = await bd.Veterinarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veterin == null)
            {
                return NotFound();
            }

            return View(veterin);
        }

        // POST: Veterins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterin = await bd.Veterinarios.FindAsync(id);
            bd.Veterinarios.Remove(veterin);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinExists(int id)
        {
            return bd.Veterinarios.Any(e => e.ID == id);
        }
    }
}
