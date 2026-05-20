using GestaoTitulos.Application.Commands;
using GestaoTitulos.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoTitulos.API.Controllers
{
    [ApiController]
    [Route("api/titulos")]
    public class TituloController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TituloController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterTitulosQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar(CriarTituloCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            //return CreatedAtAction(nameof(ObterPorNumero), new { codigo = result.Data }, result.Data);
            return Created();
        }

        [HttpGet("{numeroTitulo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorNumero(string numeroTitulo, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterTituloPorNumeroQuery(numeroTitulo), cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{numeroTitulo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Excluir(string numeroTitulo)
        {
            var result = await _mediator.Send(new ExcluirTituloCommand(numeroTitulo));

            if (!result.IsSuccess)
                return NotFound(result);

            return NoContent();
        }
    }
}
