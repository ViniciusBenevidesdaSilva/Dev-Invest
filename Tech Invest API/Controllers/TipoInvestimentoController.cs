using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.ViewModel;

namespace Tech_Invest_API.Controllers;

[Route("[controller]")]
[ApiController]
public class TipoInvestimentoController : ControllerBase
{
    private readonly ITipoInvestimentoService _tipoInvestimentoService;

    public TipoInvestimentoController(ITipoInvestimentoService tipoInvestimentoService)
    {
        _tipoInvestimentoService = tipoInvestimentoService;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IList<TipoInvestimentoViewModel>>> Get()
    {
        try
        {
            var tipoInvestimento = await _tipoInvestimentoService.GetAsync();
            return Ok(tipoInvestimento);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}", Name = "GetTipoInvestimentoById")]
    public async Task<ActionResult<TipoInvestimentoViewModel>> GetById(int id)
    {
        try
        {
            var tipoInvestimento = await _tipoInvestimentoService.GetByIdAsync(id);
            if (tipoInvestimento is null)
                return NotFound();

            return Ok(tipoInvestimento);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("GetByNome/{nome}", Name = "GetByNome")]
    public async Task<ActionResult<TipoInvestimentoViewModel>> GetByNome(string nome)
    {
        try
        {
            var tipoInvestimento = await _tipoInvestimentoService.GetByNomelAsync(nome);
            if (tipoInvestimento is null)
                return NotFound();

            return Ok(tipoInvestimento);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TipoInvestimentoViewModel tipoInvestimento)
    {
        try
        {
            var id = await _tipoInvestimentoService.CreateAsync(tipoInvestimento);
            return CreatedAtRoute("GetTipoInvestimentoById", new { id = id }, tipoInvestimento);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] TipoInvestimentoViewModel tipoInvestimento, int id)
    {
        try
        {
            await _tipoInvestimentoService.UpdateAsync(tipoInvestimento, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _tipoInvestimentoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
