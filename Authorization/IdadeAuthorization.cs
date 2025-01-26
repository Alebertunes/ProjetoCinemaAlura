using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UsuarioApi.Authorization;

public class IdadeAuthorization :
AuthorizationHandler<IdadeMinima>
{
    protected override Task HandleRequirementAsync
    (AuthorizationHandlerContext context, 
    IdadeMinima requirement)
    {
        var DataNascimentoClaim = context.User.FindFirst(clain => clain.Type == ClaimTypes.DateOfBirth);

        if(DataNascimentoClaim == null) return Task.CompletedTask;

        var dataNascimento = Convert.ToDateTime(DataNascimentoClaim.Value);

        var idadeUsuario = DateTime.Today.Year - dataNascimento.Year;
        if(dataNascimento > DateTime.Today.AddYears(-idadeUsuario)) 
        idadeUsuario--;

        if(idadeUsuario >= requirement.Idade) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}