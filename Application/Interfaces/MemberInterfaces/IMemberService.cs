using Application.DTOs.MemberDto;
using Domain.Entities;

namespace Application.Interfaces.MemberInterfaces;

public interface IMemberService
{
    Task<IEnumerable<ReadMemberDto>> GetAllMembersAsync();
    Task<ReadMemberDto> GetMemberByIdAsync(int id);
    Task<ReadMemberDto> CreateMemberAsync(CreateMemberDto member);
    Task<ReadMemberDto> UpdateMemberAsync(int id, UpdateMemberDto member);
    Task<bool> DeleteMemberAsync(int id);
}
