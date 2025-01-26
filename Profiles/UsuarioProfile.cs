using AutoMapper;
using UsuarioApi.Data.Dto;
using UsuarioApi.Models;

namespace UsuarioApi.Profiles;
public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreatedUsuarioDto, Usuario>();
    }
}