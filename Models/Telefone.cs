namespace CrudTest.Models;

public class Telefone
{
    public int TelefoneId { get; set; }
    public int UsuarioID { get; set; }
    public string NumeroTelefone { get; set; }
    public string Situacao { get; set; }
    
    public Usuario? Usuario { get; set; }
}