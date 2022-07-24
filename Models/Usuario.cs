
using System.ComponentModel.DataAnnotations;

namespace CrudTest.Models;

public class Usuario
{
    public int UsuarioId { get; set; }
    
    [Required]
    public string Nome { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Display(Name = "Data de Nascimento")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    public ICollection<Telefone>? Telefones { get; set; }
    public Endereco? Endereco { get; set; }
    public DocumentoIdentificacao? DocumentoIdentificacao  { get; set; }
}