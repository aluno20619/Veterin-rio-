using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vet.Data;
using Vet.Models;

namespace Vet.Controllers
{
    [Authorize]// fecha o acesso a qualquer recurso da classe a Util nao autenticados
    public class VeterinsController : Controller
    {
        private readonly VetsDB bd;
        private readonly IWebHostEnvironment _ambiente;

        public VeterinsController(VetsDB context)
        {
            bd = context;
            _ambiente = _ambiente;
        }

        // GET: Veterins
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await bd.Veterinarios.ToListAsync());
        }

        // GET: Veterins/Details/5
        /// <summary>
        /// Mostra dados de um veterinario.Acesso com lazy loading
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var veterinario = await bd.Veterinarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veterinario == null)
            {
                return RedirectToAction("Index");
            }

            return View(veterinario);
        }


        // GET: Veterins/Details/5
        /// <summary>
        /// Mostra dados de um veterinario .Acesso em eager loading
        /// acesso aos dados antecipadamente. select * from Consultas c,Donos d,Animais a,Veterin v
        /// where c.veterinFK =v.ID and c.AnimaisFK =a.id and a.DonosFk = d.id and v.id = id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var veterinario = await bd.Veterinarios
                .Include(v=>v.Listconsultas)
                .ThenInclude(a=>a.Animal)
                .ThenInclude(d=>d.Dono)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (veterinario == null)
            {
                return RedirectToAction("Index");
            }

            return View(veterinario);
        }

        /// <summary>
        /// Apresenta o formulario de criacao de um novo veterinario
        /// </summary>
        /// <returns></returns>
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
        public async Task<IActionResult> Create([Bind("ID,Nome,NumCedulaProf,Fotografia")] Veterin veterinario,IFormFile FotoVet)
        {
            //***********************************************
            //processar o ficheiro da fotografia
            //***********************************************
            //vars aux
            string camCompleto = "";
            bool haImagem = false;
            //se nao houver ficheiro
            if (FotoVet == null)
            {
                // ModelState.AddModelError("", "Nao se esqueça de adicionar uma foto");
                veterinario.Fotografia = "noName.png";
            }
            else { 
                if(FotoVet.ContentType==("image/jpeg")|| FotoVet.ContentType == ("image/png"))
                {
                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(FotoVet.FileName).ToLower();

                    //nome do ficheio
                    string nome = g.ToString() + extensao;
                    //identificar a extensao do ficheiro
                    //identificar o caminho do ficheiro a gravar
                    //
                    camCompleto = Path.Combine(_ambiente.WebRootPath, "Imagens\\Vets",nome);
                    veterinario.Fotografia = nome;
                    
                }
                else
                {
                    //ha ficheiro mas nao e imagem
                }
            }
            if (ModelState.IsValid)
            {

                //add o vet ao modelo
                bd.Add(veterinario);
                //consolida as alteracoes na bd
                await bd.SaveChangesAsync();
                //o fich e guardado no disco
                if (haImagem)
                {
                    using var stream = new FileStream(camCompleto, FileMode.Create);
                    await FotoVet.CopyToAsync(stream);
                }

                //redireciona o utilizador para o Index
                return RedirectToAction(nameof(Index));
            }
            // se o modelo nao valido devolve o controlo ao create
            return View(veterinario);
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
