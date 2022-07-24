using System.ComponentModel.DataAnnotations;

namespace CrudTest.Models;

public class Endereco
{
    public int EnderecoId { get; set; }
    public int UsuarioId { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    
    [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Cep Invalido")]
    public string Cep { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Complemento { get; set; }
    public string Situacao { get; set; }
    
    public Usuario? Usuario { get; set; }
}