using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.ViewModel;

namespace Tech_Invest_API.Controllers;

[Route("[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsuarioService _usuarioService;

    public TokenController(IConfiguration configuration, IUsuarioService usuarioService)
    {
        _configuration = configuration;
        _usuarioService = usuarioService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RequestToken([FromBody] UsuarioViewModel request)
    {
        try
        {
            request = await _usuarioService.AutenticarUsuario(request);
            if (request is null)
                return BadRequest("Credenciais inválidas");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro ao autenticar o usuário: " + ex.Message);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Nome!),
            new Claim(ClaimTypes.Email, request.Email!),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.NameIdentifier, request.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credenciais
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}
