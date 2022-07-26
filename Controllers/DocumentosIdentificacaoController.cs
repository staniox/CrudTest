using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudTest.Data;
using CrudTest.Models;
using CrudTest.Repositories.Interfaces;

namespace CrudTest.Controllers
{
    public class DocumentosIdentificacaoController : Controller
    {
        private readonly AgendaContext _context;
        private IUsuarioRepository _usuarioRepository;

        public DocumentosIdentificacaoController(AgendaContext context, IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _usuarioRepository = usuarioRepository;
        }

        private SelectList getSelectListUsuarios()
        {
            return new SelectList(_context.Usuarios, "UsuarioId", "Nome");
        }

        // GET: DocumentosIdentificacao
        public async Task<IActionResult> Index()
        {
            var agendaContext = _context.DocumentoIdentificacoes.Include(d => d.Usuario);
            return View(await agendaContext.ToListAsync());
        }

        // GET: DocumentosIdentificacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DocumentoIdentificacoes == null)
            {
                return NotFound();
            }

            var documentoIdentificacao = await _context.DocumentoIdentificacoes
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.DocumentoIdentificacaoId == id);
            if (documentoIdentificacao == null)
            {
                return NotFound();
            }

            return View(documentoIdentificacao);
        }

        // GET: DocumentosIdentificacao/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View();
        }

        // POST: DocumentosIdentificacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,TipoDocumento,NumeroDocumento")] DocumentoIdentificacao documentoIdentificacao)
        {
            documentoIdentificacao.Usuario = _context.Usuarios.Find(documentoIdentificacao.UsuarioId) ?? null;
            if (ModelState.IsValid && _context.Usuarios.Any(u => u.UsuarioId == documentoIdentificacao.UsuarioId))
            {
                try
                {
                    _context.Add(documentoIdentificacao);
                    await _context.SaveChangesAsync(); 
                    TempData["Message"] = "Documento Criado com sucesso";

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ViewBag.Error = true;
                    ViewBag.Message = "O Usuario ja possui Documento Cadastrado!" ;
                }
               
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Erro ao Criar Documento. Verifique se o Usuario ja possui Documento Cadastrado" ;
            }
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View(documentoIdentificacao);
        }

        // GET: DocumentosIdentificacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocumentoIdentificacoes == null)
            {
                return NotFound();
            }

            var documentoIdentificacao = await _context.DocumentoIdentificacoes.FindAsync(id);
            if (documentoIdentificacao == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View(documentoIdentificacao);
        }

        // POST: DocumentosIdentificacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentoIdentificacaoId,UsuarioId,TipoDocumento,NumeroDocumento")] DocumentoIdentificacao documentoIdentificacao)
        {
            if (id != documentoIdentificacao.DocumentoIdentificacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentoIdentificacao);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Documento Alterado com sucesso";
                }
                
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentoIdentificacaoExists(documentoIdentificacao.DocumentoIdentificacaoId))
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
                    return View(documentoIdentificacao);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Erro ao alterar Documento";
            }
            ViewData["UsuarioId"] = _usuarioRepository.GetSelectList();
            return View(documentoIdentificacao);
        }

        // GET: DocumentosIdentificacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DocumentoIdentificacoes == null)
            {
                return NotFound();
            }

            var documentoIdentificacao = await _context.DocumentoIdentificacoes
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.DocumentoIdentificacaoId == id);
            if (documentoIdentificacao == null)
            {
                return NotFound();
            }

            return View(documentoIdentificacao);
        }

        // POST: DocumentosIdentificacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DocumentoIdentificacoes == null)
            {
                return Problem("Entity set 'AgendaContext.DocumentoIdentificacoes'  is null.");
            }
            var documentoIdentificacao = await _context.DocumentoIdentificacoes.FindAsync(id);
            if (documentoIdentificacao != null)
            {
                TempData["Message"] = "Usuario removido com sucesso";
                _context.DocumentoIdentificacoes.Remove(documentoIdentificacao);
            } 
            else
            {
                ViewBag.Error = true;
                ViewBag.Message ="Erro ao remover funcionario";
            }
            
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentoIdentificacaoExists(int id)
        {
          return (_context.DocumentoIdentificacoes?.Any(e => e.DocumentoIdentificacaoId == id)).GetValueOrDefault();
        }
    }
}
