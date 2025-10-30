using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Leads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunTrack.API.Services.Leads
{
    public interface ILeadsService
    {
        Task<IEnumerable<LeadVM>> GetAllLeadsAsync(SearchVM search);
      
        Task<bool> AddLeadAsync(LeadCreateVM newLead);

        Task<bool> AddLeadWithCustomerAsync(LeadWithCustomerVM newLead);


        Task<bool> UpdateLeadAsync( LeadCreateVM updatedLead);

        Task<bool> DeleteLeadAsync(int id);



    }
}
