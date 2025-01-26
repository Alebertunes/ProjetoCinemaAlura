using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuarioApi.Controller;

[ApiController]
[Route("acesso")]
public class AcessoController : ControllerBase
{

    [HttpGet]
    [Authorize(Policy = "idadeMinima")]
    public IActionResult Get()
    {
        return Ok("Acesso Permitido!");
    }

}