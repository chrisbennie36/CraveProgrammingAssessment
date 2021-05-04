using Domains.Products.Commands;
using Domains.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        //[Authorize(AuthenticationSchemes = "Hmac")]

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a Product using the supplied ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}", Name = "GetProduct")]
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
        /// Adds a new Product
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody, Required] AddProductCommand command,
            CancellationToken cancellationToken)
        {
            var productId = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(productId);
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
        public async Task<IActionResult> DeactivateProduct(Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeactivateProductCommand { Id = id };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok();
        }
    }
}
