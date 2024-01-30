using Microsoft.AspNetCore.Mvc;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.ViewModel;

namespace Tech_Invest_API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}", Name = "GetUsuarioById")]
    public async Task<ActionResult<UsuarioViewModel>> GetById(int id)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetByEmail/{email}", Name = "GetByEmail")]
    public async Task<ActionResult<UsuarioViewModel>> GetByEmail(string email)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuarioByEmailAsync(email);
            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Autenticar")]
    public async Task<ActionResult<bool>> AutenticarUsuario([FromBody] UsuarioViewModel usuario)
    {
        try
        {
            var usuarioAutenticado = await _usuarioService.AutenticarUsuario(usuario);

            return Ok(usuarioAutenticado != null);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UsuarioViewModel usuario)
    {
        try
        {
            var id = await _usuarioService.CreateAsync(usuario);
            return CreatedAtRoute("GetUsuarioById", new { id = id }, usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
