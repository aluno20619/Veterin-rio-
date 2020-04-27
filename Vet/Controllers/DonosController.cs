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
    public class DonosController : Controller
    {
        private readonly VetsDB bd;//em sql <=> use VetsDb

        public DonosController(VetsDB context)
        {
            bd = context;
        }

        // GET: Donos
        public async Task<IActionResult> Index()
        {
            //db.Donos.ToLIstArray() <=> em SQL,select * from Donos;
            //LINQ
            //
            return View(await bd.Donos.ToListAsync());
        }

        // GET: Donos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dono = await bd.Donos.FirstOrDefaultAsync(d => d.ID == id);

           // string aux = dono.Nome.ToLower;

            if (dono == null)
            {
                return NotFound();
            }

            return View(dono);
        }

        // GET: Donos/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,NIF")] Donos dono)
        {
            if (ModelState.IsValid)
            {
                bd.Add(dono);//insert into donos value
                await bd.SaveChangesAsync();//commit
                return RedirectToAction(nameof(Index));
            }//rollback
            return View(dono);
            //viewbag
        }

        // GET: Donos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dono = await bd.Donos.FindAsync(id);
            if (dono == null)
            {
                return NotFound();
            }
            return View(dono);
        }

        // POST: Donos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,NIF")] Donos dono)
        {
            if (id != dono.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(dono);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonosExists(dono.ID))
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
            return View(dono);
        }

        // GET: Donos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dono = await bd.Donos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dono == null)
            {
                return NotFound();
            }

            return View(dono);
        }

        // POST: Donos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dono = await bd.Donos.FindAsync(id);
            bd.Donos.Remove(dono);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonosExists(int id)
        {
            return bd.Donos.Any(e => e.ID == id);
        }
    }
}
