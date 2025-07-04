using System;
using API.DTOs;
using API.Models;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUsernamesync(string username);

    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto?> GetMemberAsync(string username);
}
