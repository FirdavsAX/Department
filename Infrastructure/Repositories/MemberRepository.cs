using Application.Interfaces.MemberInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;
    public MemberRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext 
            ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }
    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        var members = await _context.Members.ToListAsync();
        return members;
    }
    public async Task<Member> GetMemberByIdAsync(int id)
    {
        Member? member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        return member;
    }

    public async Task<Member> UpdateMemberAsync(Member member)
    {
         _context.Members.Update(member);
         await _context.SaveChangesAsync();
        return member;
    }
    public async Task<Member> CreateMemberAsync(Member member)
    {
       _context.Members.Add(member);
         await _context.SaveChangesAsync();
        return member;
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        if (member == null)
        {
            throw new KeyNotFoundException($"Member with id {id} not found.");
        }
        _context.Members.Remove(member!);
        await _context.SaveChangesAsync();

        return true;
    }
    
}
