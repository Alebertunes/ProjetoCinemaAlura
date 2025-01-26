using System.ComponentModel.DataAnnotations;

namespace UsuarioApi.Data.Dto;
public class LoginUsuarioDto
{
    [Required]
    public string Username { get; 
    set; }
    [Required]
    public string Password { get; 
    set; }
}