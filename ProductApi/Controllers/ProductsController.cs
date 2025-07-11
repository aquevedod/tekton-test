using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Products.Commands.CreateProduct;
using ProductApi.Application.Products.Commands.UpdateProduct;
using ProductApi.Application.Products.Queries.GetProductById;
using ProductApi.Application.Products.Dtos;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.ProductId)
            return BadRequest("ID in route does not match ID in body");

        await _mediator.Send(command);
        return NoContent();
    }
}
