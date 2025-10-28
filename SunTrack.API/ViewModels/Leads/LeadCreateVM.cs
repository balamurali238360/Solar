using System;

namespace SunTrack.API.ViewModels.Leads
{
    public class LeadCreateVM
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string LeadNo { get; set; }
        public string Source { get; set; }
        public int StatusId { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public bool IsActive { get; set; }  
        public int Created_By { get; set; }
    }
}
