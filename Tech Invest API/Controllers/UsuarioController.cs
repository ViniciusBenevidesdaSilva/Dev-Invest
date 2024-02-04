using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IList<UsuarioViewModel>>> Get()
    {
        try
        {
            var usuario = await _usuarioService.GetAsync();
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}", Name = "GetUsuarioById")]
    public async Task<ActionResult<UsuarioViewModel>> GetById(int id)
    {
        try
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("GetByEmail/{email}", Name = "GetByEmail")]
    public async Task<ActionResult<UsuarioViewModel>> GetByEmail(string email)
    {
        try
        {
            var usuario = await _usuarioService.GetByEmailAsync(email);
            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "UpdateUsuarioPolicy")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] UsuarioViewModel usuario, int id)
    {
        try
        {
            await _usuarioService.UpdateAsync(usuario, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
