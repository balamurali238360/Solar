using Microsoft.AspNetCore.Mvc;
using SunTrack.API.Services.Leads;
using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Leads;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunTrack.API.Controllers.Leads
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadsService _leadsService;

        public LeadsController(ILeadsService leadsService)
        {
            _leadsService = leadsService;
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(IEnumerable<LeadVM>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<LeadVM>>> GetAllLeads([FromBody] SearchVM search)
        {
            try
            {
                var leads = await _leadsService.GetAllLeadsAsync(search);
                return Ok(leads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving leads: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("AddLead")]
        public async Task<ActionResult<bool>> AddLead(LeadCreateVM newLead)
        {
            try
            {
                if (newLead == null)
                    return BadRequest("Lead data is required.");

                var result = await _leadsService.AddLeadAsync(newLead);

                if (result)
                    return Ok("Lead added successfully.");
                else
                    return StatusCode(500, "Failed to add lead.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error adding lead: " + ex.Message);
            }
        }

        [HttpPut("UpdateLead")]
        public async Task<ActionResult<bool>> UpdateLead( LeadCreateVM updatedLead)

        {
            try
            {
                if (updatedLead == null)
                    return BadRequest("Lead data is required.");

                var result = await _leadsService.UpdateLeadAsync( updatedLead);

                if (result)
                    return Ok("Lead updated successfully.");
                else
                    return NotFound("Lead not found or update failed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating lead: " + ex.Message);
            }
        }

        [HttpDelete("DeleteLead/{id}")]
        public async Task<ActionResult> DeleteLead(int id)
        {
            var result = await _leadsService.DeleteLeadAsync(id);
            if (!result)
                return NotFound("Lead not found or could not be deleted.");

            return Ok("Lead deleted successfully.");
        }


    }
}
