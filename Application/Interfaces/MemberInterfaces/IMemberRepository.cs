using Domain.Entities;
namespace Application.Interfaces.MemberInterfaces;
public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<Member> GetMemberByIdAsync(int id);
    Task<Member> CreateMemberAsync(Member member);
    Task<Member> UpdateMemberAsync(Member member);
    Task<bool> DeleteMemberAsync(int id);
}
