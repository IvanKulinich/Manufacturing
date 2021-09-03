using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Albelli.OrderManagement.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("place")]
        [ProducesResponseType(typeof(Entities.Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PlaceOrder([FromBody] IEnumerable<OrderLineVM> items)
        {
            try
            {
                var order = await _orderService.AddAsync(items);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int orderId)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound($"Order with id {orderId} not found.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
