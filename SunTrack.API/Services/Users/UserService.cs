using Microsoft.EntityFrameworkCore;
using SunTrack.API.Data.Models; // 👈 adjust this if your scaffold placed models under Data.Models
using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SunTrack.API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly SunTrackContext _context;

        public UserService(SunTrackContext context)
        {
            _context = context;
        }

        public async Task<List<UserVM>> GetAllUsersAsync(SearchVM search)
        {
            var query = _context.Users 
                .Where(u => u.IsActive == true || u.IsActive == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.searchString))
            {
                query = query.Where(u =>
                    u.UserName.Contains(search.searchString) ||
                    u.Email.Contains(search.searchString) ||
                    u.Id.ToString().Contains(search.searchString));
            }

            int pageNo = search.Pageno ?? 1;
            int pageSize = search.PageSize ?? 10;

            query = query
                .OrderByDescending(u => u.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize);

            return await query.Select(u => new UserVM
            {
                Id = u.Id,
                UserName = u.UserName,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Gender = u.Gender,
                DateOfBirth = u.DateOfBirth
            }).ToListAsync();
        }

        public async Task<bool> AddUserAsync(UserVM user)
        {
            var entity = new User
            {
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                IsActive = true, 
            };

            await _context.Users.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(UserVM user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null) return false;

            existing.UserName = user.UserName;
            existing.Password = user.Password;
            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            existing.Email = user.Email;
            existing.PhoneNumber = user.PhoneNumber;
            existing.Gender = user.Gender;
            existing.DateOfBirth = user.DateOfBirth;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return false;


            existing.IsActive = false;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
