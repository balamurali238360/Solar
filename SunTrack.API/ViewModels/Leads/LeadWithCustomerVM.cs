namespace SunTrack.API.ViewModels.Leads
{
    public class LeadWithCustomerVM
    {
        public string LeadNo { get; set; } = null!;
        public string Source { get; set; } = null!;
        public int StatusId { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? EmailId { get; set; }
        public string? Phone { get; set; }
    }
}
