using CrudTest.Data;
using CrudTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrudTest.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AgendaContext _context;
    public UsuarioRepository(AgendaContext context)
    {
        _context = context;
    }

    public SelectList GetSelectList()
    {
        return new SelectList(
            _context.Usuarios, 
            "UsuarioId",
            "Email");
    }
}