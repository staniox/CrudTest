

using System.ComponentModel.DataAnnotations;

namespace CrudTest.Models;

public class DocumentoIdentificacao
{
    public int DocumentoIdentificacaoId { get; set; }
    
    [Display(Name = "Usuario")]
    [Required]
    public int UsuarioId { get; set; }
    
    [Display(Name = "Tipo de Documento")]
    [Required]
    public string TipoDocumento { get; set; }
    
    [Display(Name = "Numero de Documento")]
    [Required]
    public string NumeroDocumento { get; set; }
    
    public Usuario? Usuario { get; set; }
}