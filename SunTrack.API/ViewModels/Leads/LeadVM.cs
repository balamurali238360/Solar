using SunTrack.API.Enum;
using SunTrack.API.ViewModels;

namespace SunTrack.API.ViewModels.Leads
{
    public class LeadVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string LeadNo { get; set; } = null!;

        public string CustomerName { get; set; } = null!;


        public string EmailId { get; set; } = null!;

        public string Source { get; set; } = null!;

        public int StatusId { get; set; } 

        public DateTime FollowUpDate { get; set; }

       
        public string Phone { get; set; }


    }
}
