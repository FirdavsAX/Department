using Application.DTOs.MemberDto;
using Application.Interfaces.MemberInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService)
        {
            _memberService = memberService 
                ?? throw new ArgumentNullException(nameof(memberService), "Member service cannot be null.");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GettAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            if (members == null || !members.Any())
            {
                return NotFound("No members found.");
            }
            return Ok(members);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound($"Member with id {id} not found.");
            }
            return Ok(member);
        }
        [HttpPost]
        public async Task<ActionResult<ReadMemberDto>> CreateMember([FromBody] CreateMemberDto member)
        {
           if(member == null)
            {
                return BadRequest("Member cannot be null.");
            }
           var createdMember = await _memberService.CreateMemberAsync(member);
            if (createdMember == null)
            {
                return BadRequest("Failed to create member.");
            }
            return CreatedAtAction(nameof(GetMemberById), new { id = createdMember.Id }, createdMember);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Member>> UpdateMember(int id, [FromBody] UpdateMemberDto member)
        {
            if(string.IsNullOrEmpty(member.FullName) && string.IsNullOrEmpty(member.Degree))
            {
                return BadRequest("Member cannot be null or empty.");
            }
            if(id < 0)
            {
                return BadRequest("Invalid member id.");
            }
            var updatedMember = await _memberService.UpdateMemberAsync(id, member);
            if (updatedMember == null)
            {
                return NotFound($"Member with id {id} not found.");
            }
            return Ok(updatedMember);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid member id.");
            }
            var isDeleted = await _memberService.DeleteMemberAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Member with id {id} not found.");
            }
            return NoContent();
        }

    }
}
