using API.DTO;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository: IBaseRepository<AppUser>
{
    Task<AppUser?> GetUserByIdAsync(int id);

    Task<AppUser?> GetUserByUsernameAsync(string username);

    Task<MemberDto?> GetMemberByIdAsync(int id);

    Task<MemberDto?> GetMemberByUsernameAsync(string username);

    Task<IEnumerable<MemberDto>> GetAllMembersAsync();
}
