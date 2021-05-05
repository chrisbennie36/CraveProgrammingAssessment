using Domains.Ordering.Commands.Orders;
using Domains.Ordering.Queries.Orders;
using Domains.Ordering.QueryModels.Orders;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderQueryModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderQueryModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrders([FromBody, Required] GetOrdersByFilterQuery query)
        {
            if (query == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(query, new CancellationToken()).ConfigureAwait(false);

            if (result == null)
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
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Guid>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrders([FromBody, Required] PlaceOrdersCommand command,
            CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderIds = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(orderIds);
        }
    }
}
