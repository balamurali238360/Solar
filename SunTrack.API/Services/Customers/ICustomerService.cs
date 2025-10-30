using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunTrack.API.Services.Customers
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerVM>> GetAllCustomersAsync(SearchVM search);
        Task<bool> AddCustomerAsync(CustomerVM customer);
        Task<bool> UpdateCustomerAsync(CustomerVM customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
