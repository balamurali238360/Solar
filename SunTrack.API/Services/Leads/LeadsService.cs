using Microsoft.EntityFrameworkCore;
using SunTrack.API.Data.Models;
using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunTrack.API.Services.Leads
{
    public class LeadsService : ILeadsService
    {
        private readonly SunTrackContext _context;

        public LeadsService(SunTrackContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeadVM>> GetAllLeadsAsync(SearchVM search)
        {
            try
            {
                var query = _context.Leads
                    .Include(l => l.Customer)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(search.searchString))
                {
                    query = query.Where(l =>
                        l.Customer.CustomerName.Contains(search.searchString) ||
                        l.Source.Contains(search.searchString) ||
                        l.LeadNo.Contains(search.searchString)
                    );
                }

                int pageNo = search.Pageno ?? 1;
                int pageSize = search.PageSize ?? 10;

                var leads = await query
                     .OrderBy(l => l.Id)
                     .Skip((pageNo - 1) * pageSize)
                     .Take(pageSize)
                     .Select(l => new LeadVM
                       {
                             Id = l.Id,
                             CustomerId = l.CustomerId,
                             LeadNo = l.LeadNo,
                             Source = l.Source,
                             StatusId = l.StatusId,
                             FollowUpDate = l.FollowUpDate,
                             CustomerName = l.Customer.CustomerName,
                             EmailId = l.Customer.EmailId,
                             Phone = l.Customer.Phone 
                         })

    .ToListAsync();


                return leads;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching leads: " + ex.Message);
                return new List<LeadVM>();
            }
        }

        public async Task<bool> AddLeadAsync(LeadCreateVM newLead)
        {
            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == newLead.CustomerId);

                if (customer == null)
                {
                    Console.WriteLine("Invalid CustomerId. Customer does not exist.");
                    return false;
                }

                var lead = new Lead
                {
                    CreatedBy=1,
                    CustomerId = newLead.CustomerId,
                    LeadNo = newLead.LeadNo,
                    Source = newLead.Source,
                    StatusId = newLead.StatusId,
                    FollowUpDate = (DateTime)newLead.FollowUpDate,
                    CreatedDate = DateTime.Now
                };

                await _context.Leads.AddAsync(lead);
                await _context.SaveChangesAsync();
                    
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding lead: " + ex.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateLeadAsync( LeadCreateVM updatedLead)
        {
            try
            {
                var existingLead = await _context.Leads.FirstOrDefaultAsync(l => l.Id == updatedLead.Id);
                if (existingLead == null)
                {
                    Console.WriteLine("Lead not found.");
                    return false;
                }

                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == updatedLead.CustomerId);
                if (customer == null)
                {
                    Console.WriteLine("Invalid CustomerId. Customer does not exist.");
                    return false;
                }
                existingLead.Id = updatedLead.Id;
                existingLead.CustomerId = updatedLead.CustomerId;
                existingLead.LeadNo = updatedLead.LeadNo;
                existingLead.Source = updatedLead.Source;
                existingLead.StatusId = updatedLead.StatusId;
                existingLead.FollowUpDate = (DateTime)updatedLead.FollowUpDate;

                _context.Leads.Update(existingLead);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating lead: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteLeadAsync(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
                return false;

            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool BadRequest(object value)
        {
            throw new NotImplementedException();
        }
    }
}
