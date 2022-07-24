using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudTest.Data;
using CrudTest.Models;
using CrudTest.Repositories.Interfaces;

namespace CrudTest.Controllers
{
    public class TelefonesController : Controller
    {
        private readonly AgendaContext _context;
        private readonly IUsuarioRepository _usuarioRepository;

        public TelefonesController(AgendaContext context, IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _usuarioRepository = usuarioRepository;
        }

        // GET: Telefones
        public async Task<IActionResult> Index()
        {
            var agendaContext = _context.Telefones.Include(t => t.Usuario);
            return View(await agendaContext.ToListAsync());
        }

        // GET: Telefones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Telefones == null)
            {
                return NotFound();
            }

            var telefone = await _context.Telefones
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TelefoneId == id);
            if (telefone == null)
            {
                return NotFound();
            }

            return View(telefone);
        }

        // GET: Telefones/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = _usuarioRepository.GetSelectList();
            return View();
        }

        // POST: Telefones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioID,NumeroTelefone,Situacao")] Telefone telefone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telefone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioID"] = _usuarioRepository.GetSelectList();
            return View(telefone);
        }

        // GET: Telefones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Telefones == null)
            {
                return NotFound();
            }

            var telefone = await _context.Telefones.FindAsync(id);
            if (telefone == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = _usuarioRepository.GetSelectList();
            return View(telefone);
        }

        // POST: Telefones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TelefoneId,UsuarioID,NumeroTelefone,Situacao")] Telefone telefone)
        {
            if (id != telefone.TelefoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefoneExists(telefone.TelefoneId))
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
            ViewData["UsuarioID"] = _usuarioRepository.GetSelectList();
            return View(telefone);
        }

        // GET: Telefones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Telefones == null)
            {
                return NotFound();
            }

            var telefone = await _context.Telefones
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TelefoneId == id);
            if (telefone == null)
            {
                return NotFound();
            }

            return View(telefone);
        }

        // POST: Telefones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Telefones == null)
            {
                return Problem("Entity set 'AgendaContext.Telefones'  is null.");
            }
            var telefone = await _context.Telefones.FindAsync(id);
            if (telefone != null)
            {
                _context.Telefones.Remove(telefone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelefoneExists(int id)
        {
          return (_context.Telefones?.Any(e => e.TelefoneId == id)).GetValueOrDefault();
        }
    }
}
