using Domains.Ordering.Commands.ServiceMethods;
using Domains.Ordering.Queries.ServiceMethods;
using Domains.Ordering.QueryModels.ServiceMethods;
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
    public class ServiceMethodController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceMethodController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieves all Service Methods
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ServiceMethodQueryModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServiceMethods()
        {
            var getServiceMethodsQuery = new GetServiceMethodsQuery();

            var result = await _mediator.Send(getServiceMethodsQuery, new CancellationToken()).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Adds a new Service Method
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromBody, Required] AddServiceMethodCommand command,
            CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var serviceMethodId = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(serviceMethodId);
        }
    }
}
