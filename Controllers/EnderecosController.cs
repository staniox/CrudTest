using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudTest.Data;
using CrudTest.Models;
using CrudTest.Repositories.Interfaces;

namespace CrudTest.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly AgendaContext _context;
        private readonly IUsuarioRepository _usuarioRepository;

        public EnderecosController(AgendaContext context, IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _usuarioRepository = usuarioRepository;
        }

        // GET: Enderecos
        public async Task<IActionResult> Index()
        {
            var agendaContext = _context.Enderecos.Include(e => e.Usuario);
            return View(await agendaContext.ToListAsync());
        }

        // GET: Enderecos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enderecos == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EnderecoId == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Enderecos/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View();
        }

        // POST: Enderecos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Logradouro,Numero,Cep,Bairro,Cidade,Estado,Complemento,Situacao")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Message"] = "Documento criado com sucesso";
                    _context.Add(endereco);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ViewBag.Error = true;
                    ViewBag.Message = "O Usuario ja possui Endereco Cadastrado!" ;
                    ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
                    return View(endereco);
                }
               
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Erro ao criar Endereco. verifique se o Usuario Selecionado possui Endereco";
            }
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View(endereco);
        }

        // GET: Enderecos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enderecos == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View(endereco);
        }

        // POST: Enderecos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnderecoId,UsuarioId,Logradouro,Numero,Cep,Bairro,Cidade,Estado,Complemento,Situacao")] Endereco endereco)
        {
            if (id != endereco.EnderecoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Message"] = "Endereco Alterado com sucesso";
                    _context.Update(endereco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.EnderecoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ViewBag.Error = true;
                    ViewBag.Message = "O Usuario ja possui Documento Cadastrado!" ;
                    ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
                    return View(endereco);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Erro ao Editar Endereco. verifique se o Usuario Selecionado possui Endereco";
            }
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View(endereco);
        }

        // GET: Enderecos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enderecos == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EnderecoId == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enderecos == null)
            {
                return Problem("Entity set 'AgendaContext.Enderecos'  is null.");
            }
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco != null)
            {
                TempData["Message"] = "Endereco removido com sucesso";
                _context.Enderecos.Remove(endereco);
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Erro ao remover Endereco";
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id)
        {
          return (_context.Enderecos?.Any(e => e.EnderecoId == id)).GetValueOrDefault();
        }
    }
}
