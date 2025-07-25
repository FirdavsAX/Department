
using Application.DTOs.MemberDto;
using Application.Interfaces.MemberInterfaces;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Services;
public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }
    public async Task<IEnumerable<ReadMemberDto>> GetAllMembersAsync()
    {
        var result = await _memberRepository.GetAllMembersAsync();
        
        var readMemberDtos = result.Select(member => new ReadMemberDto
        {
            Id = member.Id,
            FullName = member.FullName,
            Degree = member.Degree,
            Info = member.Info,
            PhotoUrl = member.PhotoUrl
        }).ToList();
     
        return readMemberDtos;
    }
    public async Task<ReadMemberDto> GetMemberByIdAsync(int id)
    {
        var memberById = await _memberRepository.GetMemberByIdAsync(id);
       
        if(memberById is null)
        {
            return null;
        }
        var readMemberDto = new ReadMemberDto
        {
            Id = memberById.Id,
            FullName = memberById.FullName,
            Degree = memberById.Degree,
            Info = memberById.Info,
            PhotoUrl = memberById.PhotoUrl
        };
        return readMemberDto;
    }
    public async Task<ReadMemberDto> UpdateMemberAsync(int id, UpdateMemberDto member)
    {
        var existingMember = await _memberRepository.GetMemberByIdAsync(id);
        if (existingMember == null)
        {
            throw new KeyNotFoundException($"Member with id {id} not found.");
        }
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member), "Member cannot be null.");
        }
        existingMember.FullName = member.FullName ?? existingMember.FullName;
        existingMember.Degree = member.Degree ?? existingMember.Degree;
        existingMember.Info = member.Info ?? existingMember.Info;
        existingMember.PhotoUrl = member.PhotoUrl ?? existingMember.PhotoUrl;

        var updatedMember = await _memberRepository.UpdateMemberAsync(existingMember);
        if (updatedMember == null)
        {
            throw new InvalidOperationException("Failed to update member.");
        }
        var readMemberDto = new ReadMemberDto
        {
            Id = updatedMember.Id,
            FullName = updatedMember.FullName,
            Degree = updatedMember.Degree,
            Info = updatedMember.Info,
            PhotoUrl = updatedMember.PhotoUrl
        };

        return readMemberDto;
    }
    public async Task<ReadMemberDto> CreateMemberAsync(CreateMemberDto member)
    {
        if(member == null)
        {
            throw new ArgumentNullException(nameof(member), "Member cannot be null.");
        }
        var memberEntity = new Member
        {
            FullName = member.FullName,
            Degree = member.Degree,
            Info = member.Info,
            PhotoUrl = member.PhotoUrl
        };
        var createdMember = await _memberRepository.CreateMemberAsync(memberEntity);
        if (createdMember == null)
        {
            throw new InvalidOperationException("Failed to create member.");
        }
        var readMemberDto = new ReadMemberDto
        {
            Id = createdMember.Id,
            FullName = createdMember.FullName,
            Degree = createdMember.Degree,
            Info = createdMember.Info,
            PhotoUrl = createdMember.PhotoUrl
        };
        return readMemberDto;
    }
    public async Task<bool> DeleteMemberAsync(int id)
    {
        var member = await _memberRepository.GetMemberByIdAsync(id);

        var isDeleted = await _memberRepository.DeleteMemberAsync(id);
        if (!isDeleted)
        {
            throw new InvalidOperationException("Failed to delete member.");
        }
        return isDeleted;
    }
}
