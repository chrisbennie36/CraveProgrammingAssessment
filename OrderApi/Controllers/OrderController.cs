using Domains.Orders.Commands;
using Domains.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves an Order using the supplied ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var getOrderQuery = new GetOrderByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getOrderQuery, new CancellationToken()).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Retrieves Orders using a filter
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetOrders([FromBody, Required] GetOrdersByFilterQuery query)
        {
            if (query == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(query, new CancellationToken()).ConfigureAwait(false);

            if (result == null || !result.Orders.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Adds new Orders
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddOrders([FromBody, Required] PlaceOrdersCommand command,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderIds = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(orderIds);
        }
    }
}
