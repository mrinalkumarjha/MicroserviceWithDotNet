using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupan), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupan>> GetDiscount(string productName)
        {
            var coupan = await _discountRepository.GetDiscount(productName);
            return Ok(coupan);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupan), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupan>> CreateDiscount([FromBody] Coupan coupan)
        {
            await _discountRepository.CreateDiscount(coupan);
            return CreatedAtRoute("GetDiscount", new { productName = coupan.ProductName }, coupan)
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupan), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupan>> UpdateeDiscount([FromBody] Coupan coupan)
        {
           return Ok(await _discountRepository.UpdateDiscount(coupan));
        }

        [HttpGet("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(Coupan), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupan>> DeleteDiscount(string productName)
        {
            return Ok(await _discountRepository.DeleteDiscount(productName));
        }

    }
}
