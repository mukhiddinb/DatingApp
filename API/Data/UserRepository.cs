using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await context.Users.Include(u => u.Photos).ToListAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
    {
        return await context.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public Task<MemberDto?> GetMemberByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<MemberDto?> GetMemberByUsernameAsync(string username)
    {
        return await context.Users.Where(u => u.UserName.ToLower() == username.ToLower()).ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }
}
