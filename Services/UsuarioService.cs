using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuarioApi.Data.Dto;
using UsuarioApi.Models;
using UsuarioApi.Services;

namespace CadastroServices;

public class UsuarioService
{

    private IMapper _mapper;
    private UserManager<Usuario> _userManeger;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManeger = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Cadastra(CreatedUsuarioDto usuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

        IdentityResult resultado = await _userManeger.CreateAsync(usuario, usuarioDto.Password);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuario!");
        }

    }

    public async Task<string> Login(LoginUsuarioDto usuarioDto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(usuarioDto.Username, usuarioDto.Password, false, false); //false1 = não queremos que o cookie persista no navegador depois de fechar*/
                                                                                                                          //false2 = não queremos que bloqueie o usuario após errar a senha
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Usuario não autenticado!");
        }

        var usuario = _signInManager
        .UserManager
        .Users.FirstOrDefault(user => user.NormalizedUserName == usuarioDto.Username.ToUpper());

        var token = _tokenService.GenerateToken(usuario);

        return token;
    }

}

