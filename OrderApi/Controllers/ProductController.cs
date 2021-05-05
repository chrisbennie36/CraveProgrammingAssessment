using Domains.Ordering.Commands.Products;
using Domains.Ordering.Queries.Products;
using Domains.Ordering.QueryModels.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieves a Product using the supplied ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductQueryModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var getProductQuery = new GetProductByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getProductQuery, new CancellationToken()).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductQueryModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts()
        {
            var getProductsQuery = new GetProductsQuery();

            var result = await _mediator.Send(getProductsQuery, new CancellationToken()).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Adds a new Product
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        public async Task<IActionResult> AddProduct([FromBody, Required] AddProductCommand command,
            CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var productId = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return CreatedAtRoute("GetProduct", new { id = productId }, productId);
        }

        /// <summary>
        /// Activates an existing Product
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("activate/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ActivateProduct(Guid id,
            CancellationToken cancellationToken)
        {
            var command = new ActivateProductCommand { Id = id };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Deactivates an existing Product
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deactivate/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeactivateProduct(Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeactivateProductCommand { Id = id };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok();
        }
    }
}
