using AutoMapper;
using CadastroServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data;
using UsuarioApi.Data.Dto;
using UsuarioApi.Models;

namespace UsuarioApi.Controller;

[ApiController]
[Route("{usuarios}")]
public class UsuarioController : ControllerBase
{

    private UsuarioService _usuarioService;


    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }


    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreatedUsuarioDto usuarioDto)
    {
        await _usuarioService.Cadastra(usuarioDto);
        return Ok("Usuario cadastrado com sucesso");        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDto usuarioDto)
    {
        var token = await _usuarioService.Login(usuarioDto);
        return Ok(token);
    }
}   