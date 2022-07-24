using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrudTest.Repositories.Interfaces;

public interface IUsuarioRepository
{
    public SelectList GetSelectList();
    
}