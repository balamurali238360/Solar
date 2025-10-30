using Microsoft.EntityFrameworkCore;
using SunTrack.API.Data.Models;
using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Customers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunTrack.API.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly SunTrackContext _context;


        public CustomerService(SunTrackContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerVM>> GetAllCustomersAsync(SearchVM search)
        {
            var query = _context.Customers
           .Where(c => c.IsActive)
           .AsQueryable();

            if (!string.IsNullOrEmpty(search.searchString))
            {
                query = query.Where(c =>
                    c.CustomerName.Contains(search.searchString) ||
                    c.Phone.Contains(search.searchString) ||
                    c.Id.ToString().Contains(search.searchString));
            }

            int pageNo = search.Pageno ?? 1;
            int pageSize = search.PageSize ?? 10;

            query = query
                .OrderByDescending(c => c.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize);

            return await query
                .Select(c => new CustomerVM
                {
                    Id = c.Id,
                    CustomerName = c.CustomerName,
                    Phone = c.Phone,
                    EmailId = c.EmailId,
                    IsActive = c.IsActive,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<bool> AddCustomerAsync(CustomerVM customer)
        {
            var entity = new Customer
            {
                CustomerName = customer.CustomerName,
                Phone = customer.Phone,
                EmailId = customer.EmailId,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedBy = 1
            };

            await _context.Customers.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerVM customer)
        {
            var existing = await _context.Customers.FindAsync(customer.Id);
            if (existing == null) return false;

            existing.CustomerName = customer.CustomerName;
            existing.Phone = customer.Phone;
            existing.EmailId = customer.EmailId;
            existing.UpdatedDate = DateTime.Now;
            existing.UpdatedBy = 1;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null) return false;
            existing.IsActive = false;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
