using Microsoft.AspNetCore.Mvc;
using SunTrack.API.Services.Customers;
using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Customers;
using SunTrack.API.ViewModels.Leads;
using System.Threading.Tasks;

namespace SunTrack.API.Controllers.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(IEnumerable<CustomerVM>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<CustomerVM>>> GetAllLeads( SearchVM search)
        {
            try
            {
                var leads = await _customerService.GetAllCustomersAsync(search);
                return Ok(leads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving leads: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomer( CustomerVM customer)
        {
            var result = await _customerService.AddCustomerAsync(customer);
            return result ? Ok("Customer added successfully.") : BadRequest("Failed to add customer.");
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(CustomerVM customer)
        {
            var result = await _customerService.UpdateCustomerAsync(customer);
            return result ? Ok("Customer updated successfully.") : NotFound("Customer not found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            return result ? Ok("Customer deleted successfully.") : NotFound("Customer not found.");
        }
    }
}
