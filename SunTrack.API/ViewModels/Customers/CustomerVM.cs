namespace SunTrack.API.ViewModels.Customers
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
